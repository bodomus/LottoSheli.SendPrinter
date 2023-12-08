using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Core;
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
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Repository.LiteDB;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Prints <see cref="TicketTask"/> by specified <see cref="PrintSummaryCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IPrintSummaryCommand))]
    public class PrintSummaryCommand : PrintCommandBase, IPrintSummaryCommand
    {
        private readonly ILogger<PrintSummaryCommand> _logger;
        private readonly IRemoteClient _remoteClient;
        private readonly IPrinterDevice _printerDevice;
        private readonly ISettings _settings;
        protected readonly IUserRepository _userRepository;

        public PrintSummaryCommand(IPrinterDevice printerDevice,
            ISettings settings,
            ILogger<PrintSummaryCommand> logger,
            ITicketTaskRepository ticketTaskRepository,
            IUserRepository userRepository,
            IRemoteClient remoteClient)
            : base(ticketTaskRepository)
        {
            _logger = logger;
            _remoteClient = remoteClient;
            _printerDevice = printerDevice;
            _settings = settings;
            _userRepository = userRepository;
        }

        public bool CanExecute()
        {
            return true;
        }

        public Task<string> Execute(PrintSummaryCommandData data)
        {
            return Task.Run(async () =>
            {
                var paths = new List<string>();
                var date = DateTime.Now;

                if (null == data.Ranges || 0 == data.Ranges.Count)
                    data.AddRange(0, int.MaxValue);

                foreach (var range in data.Ranges) 
                { 
                    var path = await ExecuteRangeInternally(date, range.Item1, range.Item2);
                    if (null != path)
                        paths.Add(path);
                }

                return paths.LastOrDefault();
            });
        }

        private async Task<string> ExecuteRangeInternally(DateTime date, int start, int end) 
        {
            string report = await GenerateSummaryReportAsync(date, Math.Min(start, end), Math.Max(start, end));
            return !string.IsNullOrEmpty(report) 
                ? PrintSummary(report, date, $"_{start}_{end}") 
                : null;
        }

        private string PrintSummary(string report, DateTime date, string rangeEnd = "")
        {
            PrintSettings settings = _settings.GetPrintProfile("Summary");
            _printerDevice.PrintText(report, _settings.PrinterName, 12, 5, 14.0f, settings);

            string reportsPath = Path.Combine(SettingsManager.LottoHome, "summary_reports");
            Directory.CreateDirectory(reportsPath);


            var filename = $"{date:yyyy dd MMM HH_mm_ss}{rangeEnd}.txt";
            var fullPath = Path.Combine(reportsPath, filename);
            File.WriteAllText(fullPath, report);
            return fullPath;
        }

        /// <summary>
        /// generates summry report
        /// </summary>
        /// <param name="date">date of report</param>
        /// <param name="sequenceStart">start number in sequence</param>
        /// <param name="sequenceEnd">end number in sequence</param>
        /// <returns>string report</returns>
        public async Task<string> GenerateSummaryReportAsync(DateTime date, int? sequenceStart = null, int? sequenceEnd = null)
        {
            int stationId = int.TryParse(_settings.StationId, out int stId) ? stId : 0;
            string stationName = string.IsNullOrWhiteSpace(_settings.PrintedStationId) ? stationId.ToString() : _settings.PrintedStationId;
            int bundleId = 0;

            await _ticketTaskRepository.BackupContext();

            var (printerLogin, _) = _userRepository.GetCredentials(Role.D9);
            var reportData = GetSummaryReportData(date, printerLogin, stationId, stationName, sequenceStart ?? 0, sequenceEnd ?? int.MaxValue);

            if(!reportData.Tickets.Any())
                return null;

            var expectedRange = (sequenceStart == 0 && sequenceEnd >= int.MaxValue) ? "All" : $"{sequenceStart}-{sequenceEnd}";
            var realRange = CalculateRealRange(reportData.Tickets);


            reportData.TicketPrintType = (sequenceStart == null || sequenceEnd == null) ? TicketPrintType.All : TicketPrintType.Range;

            var summaryResponse = await _remoteClient.CreateSummaryReport(reportData);

            var items = _ticketTaskRepository
                .GetBetweenSequences(sequenceStart ?? 0, sequenceEnd ?? int.MaxValue)
                .OrderByDescending(obj => obj.Sequence)
                .Select(x => x)
                .Where(t => t != null).ToList();
            if (items.Count > 0)
                bundleId = items.First().Bundle;

            _logger.LogInformation("items length - {0}", items.Count);

            var reportItems = items
                .GroupBy(t => t.Type.GetTicketTypeValue())
                .Select(gr => new
                {
                    T = gr.Key,
                    S = gr.Sum(s => s.Price),
                    C = gr.Count()
                }).ToList();

            _logger.LogInformation("report items length - {0}", reportItems.Sum(i => i.C));

            var userIds = items.Select(l =>
            {
                if ((l.Type == TaskType.RegularChance
                    || l.Type == TaskType.Regular123
                    || l.Type == TaskType.MethodicalChance
                    || l.Type == TaskType.MultipleChance) && l.UserIdMandatoryFlag <= 0)
                {
                    return null;
                }
                return $"\u25A1 {l.UserId} {(l.Extra == true ? "x" : " ")}  {l.Type.GetEnumMemberSummaryValue()}";
            }).Where(v => !string.IsNullOrEmpty(v)).ToList();

            _logger.LogInformation("user ids length - {0}", userIds.Count);

            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(_settings.PrintedStationId))
                sb.AppendFormat("{0}; ", _settings.PrintedStationId);

            sb.AppendFormat("{0}", date);
            sb.AppendLine();
            sb.AppendLine($"Range: {expectedRange}({realRange})");
            sb.AppendLine($"Bundle: {bundleId}");

            AppendLines(sb, 1);
            sb.AppendFormat("TOTAL: {0,13}", reportItems.Sum(item => item.S));
            AppendLines(sb, 1);
            sb.AppendLine("=========================");
            AppendLines(sb, 2);
            string reportFormat = "{0,-13}|{1,-5}|{2,-20}";
            sb.AppendFormat(reportFormat, "GAME", "COUNT", "SUM");
            AppendLines(sb, 1);
            sb.AppendLine("=========================");

            foreach (var item in reportItems)
            {
                sb.AppendFormat(reportFormat, item.T, item.C, item.S);
                AppendLines(sb, 2);
            }

            sb.Append("User Ids:");
            AppendLines(sb, 2);

            sb.Append(string.Join(Environment.NewLine, userIds));
            return sb.ToString();
        }

        public string CalculateRealRange(IEnumerable<TicketData> tickets)
        {
            if (!tickets.Any())
                return "0-0";

            var sortedTickets = tickets.OrderBy((obj => obj.IId)).ToList();

            var counter = sortedTickets.Min(obj => obj.IId);

            var ranges = new List<TicketRangeData> { new TicketRangeData { From = counter, To = counter } };

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
