using LottoSheli.SendPrinter.Commands.Base;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    public class FindTicketTaskByTicketIdCommandData : ICommandData
    {
        public int TicketId { get; set; }
    }
}
