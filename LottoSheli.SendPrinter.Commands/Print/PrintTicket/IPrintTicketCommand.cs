using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print;
using LottoSheli.SendPrinter.Entity;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Prints <see cref="TicketTask"/> by specified <see cref="PrintTicketCommandData"/>
    /// </summary>
    public interface IPrintTicketCommand : IParametrizedWithResultCommand<PrintTicketCommandData, Task<PrintTicketResult>>
    {

    }
}
