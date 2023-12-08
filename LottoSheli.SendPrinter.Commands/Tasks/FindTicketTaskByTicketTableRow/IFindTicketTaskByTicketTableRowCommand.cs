using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Finds <see cref="TicketTask"/> by specified <see cref="FindTicketTaskByTicketTableRowCommandData"/>
    /// </summary>
    public interface IFindTicketTaskByTicketTableRowCommand : IParametrizedWithResultCommand<FindTicketTaskByTicketTableRowCommandData, IEnumerable<TicketTask>>
    {

    }
}
