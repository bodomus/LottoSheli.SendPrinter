using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class GetPrinterSettingsForTicketTaskCommandData : ICommandData
    {
        public TicketTask ExisintgTicketTask { get; init; }
    }
}
