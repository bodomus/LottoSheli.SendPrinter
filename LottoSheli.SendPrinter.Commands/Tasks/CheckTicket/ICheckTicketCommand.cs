using LottoSheli.SendPrinter.Commands.Base;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Recognizes ticket for specified <see cref="CheckTicketCommandData"/>
    /// </summary>
    public interface ICheckTicketCommand : IParametrizedWithResultCommand<CheckTicketCommandData, Task<CheckTicketResult>>
    {

    }
}
