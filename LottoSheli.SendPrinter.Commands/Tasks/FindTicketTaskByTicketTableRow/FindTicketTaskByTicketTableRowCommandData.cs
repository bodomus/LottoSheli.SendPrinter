using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    public class FindTicketTaskByTicketTableRowCommandData : ICommandData
    {
        public TicketTable SingleRow { get; set; }
    }
}
