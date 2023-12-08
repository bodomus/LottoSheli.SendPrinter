using LottoSheli.SendPrinter.Commands.Base;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Returns <see cref="PrintSettingsResult"/> by specified <see cref="GetPrinterSettingsForTicketTaskCommandData"/>
    /// </summary>
    public interface IGetPrinterSettingsForTicketTaskCommand : IParametrizedWithResultCommand<GetPrinterSettingsForTicketTaskCommandData, PrinterSettingsResult>
    {

    }
}
