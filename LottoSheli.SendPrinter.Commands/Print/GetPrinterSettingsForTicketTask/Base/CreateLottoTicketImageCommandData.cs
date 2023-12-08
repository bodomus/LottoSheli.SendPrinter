using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class CreateLottoTicketImageCommandData : ICommandData
    {
        public TicketTask ExisintgTicketTask { get; init; }
    }
}
