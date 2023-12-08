using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    /// <summary>
    /// Finds <see cref="TicketTask"/> by specified <see cref="FindTicketTaskByTicketIdCommandData"/>
    /// </summary>
    public interface IFindOrderedTicketTaskByTicketIdCommand : IParametrizedWithResultCommand<FindTicketTaskByTicketIdCommandData, TicketTask>
    {

    }
}
