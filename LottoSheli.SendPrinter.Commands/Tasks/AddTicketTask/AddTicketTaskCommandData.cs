using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    public class AddTicketTaskCommandData : ICommandData
    {
        public TicketTask NewTicketTask { get; init; }

        public int? Sequence { get; init; }
    }
}
