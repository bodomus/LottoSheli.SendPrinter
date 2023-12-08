using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class PrintTicketCommandData : ICommandData
    {
        public TicketTask ExistingTicketTask { get; init; }

        public bool Reprinted { get; init; }
    }
}
