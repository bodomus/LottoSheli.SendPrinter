using LottoSheli.SendPrinter.Commands.Base;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    public class FindTicketTaskBySequenceCommandData : ICommandData
    {
        public int SeqenceNumber { get; set; }
    }
}
