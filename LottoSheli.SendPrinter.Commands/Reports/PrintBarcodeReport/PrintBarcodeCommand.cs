using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Tasks;
using LottoSheli.SendPrinter.DTO.Remote;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Printer.Devices;
using LottoSheli.SendPrinter.Remote;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LottoSheli.SendPrinter.Settings.Models;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Prints <see cref="TicketTask"/> by specified <see cref="PrintBarcodeCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IPrintBarcodeCommand))]
    public class PrintBarcodeCommand : PrintCommandBase, IPrintBarcodeCommand
    {
        private readonly ILogger<PrintBarcodeCommand> _logger;

        private readonly IRemoteClient _remoteClient;
        private readonly IGetPrinterSettingsForTicketTaskCommand _getPrinterSettingsForTicketTaskCommand;
        private readonly IPrinterDevice _printerDevice;
        private readonly ISettings _settings;
        private readonly IIncreasePrintedCountCommand _increasePrintedCountCommand;

        public PrintBarcodeCommand(IGetPrinterSettingsForTicketTaskCommand getPrinterSettingsForTicketTaskCommand,
            IIncreasePrintedCountCommand increasePrintedCountCommand,
            IPrinterDevice printerDevice,
            ISettings settings,
            ILogger<PrintBarcodeCommand> logger,
            ITicketTaskRepository ticketTaskRepository,
            IRemoteClient remoteClient) 
            : base(ticketTaskRepository)
        {
            _logger = logger;
            _remoteClient = remoteClient;
            _getPrinterSettingsForTicketTaskCommand = getPrinterSettingsForTicketTaskCommand;
            _printerDevice = printerDevice;
            _settings = settings;
            _increasePrintedCountCommand = increasePrintedCountCommand;
        }

        public bool CanExecute()
        {
            return true;
        }

        public Task<string> Execute(PrintBarcodeCommandData data)
        {
            return Task.Run(async () =>
            {
                string fullPath = "";
                Tuple<long, string> reportData = null;

                var date = DateTime.Now;

                if (data.Ranges == null)
                {
                    reportData = await GenerateBarcodeReportAsync(date);
                    fullPath = PrintSummary(reportData.Item2, reportData.Item1, date);
                }
                else
                {
                    for (int i = 0; i < data.Ranges.Count; i++)
                    {
                        int start = data.Ranges[i].Item1;
                        int end = data.Ranges[i].Item2;

                        reportData = await GenerateBarcodeReportAsync(date, Math.Min(start, end), Math.Max(start, end));
                        if (reportData != null)
                        {
                            fullPath = PrintSummary(reportData.Item2, reportData.Item1, date, string.Format("_{0}_{1}", start, end));
                        }
                    }
                }

                return fullPath;
            });
        }        

        private string PrintSummary(string report, long summaryId, DateTime date, string rangeEnd = "")
        {
            PrintSettings settings = _settings.GetPrintProfile("Summary");
            _printerDevice.PrintTextWithBarcode(report, summaryId, _settings.PrinterName, 15, 5, 14.0f, settings);

            string reportsPath = Path.Combine(SettingsManager.LottoHome, "barcode_reports");
            Directory.CreateDirectory(reportsPath);


            var filename = $"{date:yyyy dd MMM HH_mm_ss}{rangeEnd}.txt";
            var fullPath = Path.Combine(reportsPath, filename);
            File.WriteAllText(fullPath, report);
            return fullPath;
        }

        public async Task<Tuple<long, string>> GenerateBarcodeReportAsync(DateTime date, int? sequenceStart = null, int? sequenceEnd = null)
        {
            int stId = 0;
            int stationId = int.TryParse(_settings.StationId, out stId) ? stId : 0;
            string stationName = string.IsNullOrWhiteSpace(_settings.PrintedStationId) ? stationId.ToString() : _settings.PrintedStationId;

            var reportData = GetSummaryReportData(date, _settings.D9AccessKey, stationId, stationName, sequenceStart ?? 0, sequenceEnd ?? int.MaxValue);

            var expectedRange = (sequenceStart == null || sequenceEnd == null) ? "All" : $"{sequenceStart}-{sequenceEnd}";
            var realRange = CalculateRealRange(reportData.Tickets);

            reportData.TicketPrintType = (sequenceStart == null || sequenceEnd == null) ? TicketPrintType.All : TicketPrintType.Range;

            var summaryResponse = await _remoteClient.CreateSummaryReport(reportData);

            if (summaryResponse == null)
                return null;

            var report = new StringBuilder();
            report.AppendLine(stationName);
            report.AppendLine(date.ToString("dd/MM/yyyy"));
            report.AppendLine(date.ToString("HH:mm:ss"));
            report.AppendLine(string.Format($"Range: {expectedRange}({realRange})"));
            report.AppendLine($"TOTAL: {reportData.TotalAmount.ToString()}");
            report.AppendLine(string.Join(Environment.NewLine, reportData.Tickets.Select(obj =>
            {
                switch (obj.GameType)
                {
                    case "methodical_chance":
                    case "multiple_chance":
                    case "regular_chance":
                        return "C";
                    case "regular_123":
                    case "combined_123":
                        return "1";
                    case "regular":
                    case "double":
                    case "social":
                        return "L";
                    case "777":
                        return "7";
                    default: throw new NotSupportedException(obj.GameType);
                }
            }).GroupBy(g => g).Select(obj => $"{obj.Key} | {obj.Count()}")));
            report.AppendLine("========================");
            report.AppendLine($"DSR ID: {summaryResponse.Id.Value.ToString("D4")}");
            report.AppendLine();

            return new Tuple<long, string>(summaryResponse.Id.Value, report.ToString());
        }

        public string CalculateRealRange(IEnumerable<TicketData> tickets)
        {
            if (tickets.Count() <= 0)
                return "0-0";

            var sortedTickets = tickets.OrderBy((obj => obj.IId)).ToList();

            var counter = sortedTickets.Min(obj => obj.IId);

            var ranges = new List<TicketRangeData>();

            ranges.Add(new TicketRangeData { From = counter, To = counter });

            foreach (var ticket in sortedTickets)
            {
                if (ticket.IId == counter)
                {
                    ranges[ranges.Count - 1].To = counter;
                    counter++;
                }
                else
                {
                    ranges.Add(new TicketRangeData { From = ticket.IId, To = ticket.IId });
                    counter = ticket.IId + 1;
                }
            }

            return string.Join(",\r\n", ranges.Select(range => range.From < range.To ? $"{range.From}-{range.To}" : range.From.ToString()));
        }

        private void AppendLines(StringBuilder sb, int times)
        {
            while (times > 0)
            {
                sb.AppendLine();
                --times;
            }
        }
    }
}
