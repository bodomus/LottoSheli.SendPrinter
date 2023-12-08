using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Add <see cref="TicketTask"/> by specified <see cref="AddTicketTaskCommandData"/>
    /// </summary>
    public interface IAddTicketTaskCommand : IParametrizedWithResultCommand<AddTicketTaskCommandData, TicketTask>
    {

    }
}
