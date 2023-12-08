using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class IncreasePrintedCountCommandData : ICommandData
    {
        public TicketTask ExistsingTicketTask { get; set; }
    }
}
