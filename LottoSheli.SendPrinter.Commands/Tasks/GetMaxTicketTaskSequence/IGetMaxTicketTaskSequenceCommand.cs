using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Returns <see cref="TicketTask"/> with max sequence number
    /// </summary>
    public interface IGetMaxTicketTaskSequenceCommand : IResultCommand<int>
    {

    }
}
