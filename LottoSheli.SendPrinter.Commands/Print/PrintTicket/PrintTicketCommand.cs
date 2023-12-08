using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Tasks;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Printer.Devices;
using LottoSheli.SendPrinter.Printer.Exceptions;
using LottoSheli.SendPrinter.Settings.Models;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using LottoSheli.SendPrinter.Core.Monitoring;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Prints <see cref="TicketTask"/> by specified <see cref="PrintTicketCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IPrintTicketCommand))]
    public class PrintTicketCommand : IPrintTicketCommand
    {
        private readonly ILogger<PrintTicketCommand> _logger;
        private readonly IGetPrinterSettingsForTicketTaskCommand _getPrinterSettingsForTicketTaskCommand;
        private readonly IPrinterDevice _printerDevice;
        private readonly ISettings _settings;
        private readonly IIncreasePrintedCountCommand _increasePrintedCountCommand;
        private readonly IMonitoringService _monitoringService;

        public PrintTicketCommand(IGetPrinterSettingsForTicketTaskCommand getPrinterSettingsForTicketTaskCommand, IIncreasePrintedCountCommand increasePrintedCountCommand, 
            IPrinterDevice printerDevice, IMonitoringService monitoringService, ISettings settings, ILogger<PrintTicketCommand> logger)
        {
            _logger = logger;
            _getPrinterSettingsForTicketTaskCommand = getPrinterSettingsForTicketTaskCommand;
            _printerDevice = printerDevice;
            _monitoringService = monitoringService;
            _settings = settings;
            _increasePrintedCountCommand = increasePrintedCountCommand;
        }

        public bool CanExecute()
        {
            return true;
        }

        public async Task<PrintTicketResult> Execute(PrintTicketCommandData data)
        {

            try
            {
                var printSettingsResult = _getPrinterSettingsForTicketTaskCommand.Execute(new GetPrinterSettingsForTicketTaskCommandData
                {
                    ExisintgTicketTask = data.ExistingTicketTask
                });
                using Bitmap bitmap = printSettingsResult.PrintedImage;
                PrintSettings settings = _settings.GetPrintProfile(printSettingsResult.PrinterSettingsProfile);

                string description = printSettingsResult.DescriptionForPrint;

                int count = 0;

                if (data.ExistingTicketTask.Sequence != -1)
                {
                    var reprintIds = default(IEnumerable<dynamic>);
                    try
                    {
                        reprintIds = (IEnumerable<dynamic>)data.ExistingTicketTask.Settings["bulk_ids"];
                    }
                    catch (Exception ex) when (ex is RuntimeBinderException || ex is KeyNotFoundException)
                    {
                        _logger.LogInformation("bulk_ids param is not found");
                    }

                    description = data.ExistingTicketTask.Sequence.ToString() + ", " + description + (string.IsNullOrWhiteSpace(_settings.PrintedStationId) ? "" : $", {_settings.PrintedStationId}");
                    count = reprintIds == null ? 0 : reprintIds.Count();
                    if (count > 1)
                    {
                        description += string.Format(",Q: {0}", count + 1); //we always must have in mind our current ticket ID
                    }
                }

                _logger.LogInformation("Printing ticket on {0}...", _settings.PrinterName);
                await _printerDevice.PrintTicketAwaitable(bitmap, _settings.PrinterName, description, settings, _settings, false);
                await _monitoringService.InformTicketPrinted();
                if (data.Reprinted)
                {
                    _increasePrintedCountCommand.Execute(new IncreasePrintedCountCommandData { ExistsingTicketTask = data.ExistingTicketTask });
                }

                return new PrintTicketResult { PrintedCountLeft = count, Status = 1 };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var exception = new PrintingException(data.ExistingTicketTask.Id, ex);
                return new PrintTicketResult { Error = exception };
            }
        }
    }
}
