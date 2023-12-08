using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Removes <see cref="TicketTask"/> by specified <see cref="RemoveTaskCommandData"/>
    /// </summary>
    public interface IRemoveTaskCommand : IParametrizedWithResultCommand<RemoveTaskCommandData, bool>
    {

    }
}
