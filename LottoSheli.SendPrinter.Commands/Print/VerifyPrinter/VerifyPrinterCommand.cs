using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Printer.Devices;
using LottoSheli.SendPrinter.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Print
{
    /// <summary>
    /// Collects printer state
    /// </summary>
    [Command(Basic = typeof(IVerifyPrinterCommand))]
    public class VerifyPrinterCommand : IVerifyPrinterCommand
    {
        private readonly IPrinterDevice _printerDevice;
        private readonly ISettings _settings;
        private readonly ILogger<IVerifyPrinterCommand> _logger;
        public VerifyPrinterCommand(IPrinterDevice printerDevice, ISettings settings, ILogger<IVerifyPrinterCommand> logger) => 
            (_printerDevice, _settings, _logger) = (printerDevice, settings, logger);
        public bool CanExecute()
        {
            return true;
        }

        public VerifyPrinterCommandResult Execute()
        {
            return new VerifyPrinterCommandResult
            { 
                PrinterName = _settings.PrinterName,
                IsInstalled = _printerDevice.VerifyPrinterInstalled(_settings.PrinterName),
                Error = _printerDevice.VerifyPrinterReady(_settings.PrinterName)
            };
        }
    }
}
