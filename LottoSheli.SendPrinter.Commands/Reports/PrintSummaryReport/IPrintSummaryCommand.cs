using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Prints <see cref="TicketTask"/> by specified <see cref="PrintSummaryCommandData"/>
    /// </summary>
    public interface IPrintSummaryCommand : IParametrizedWithResultCommand<PrintSummaryCommandData, Task<string>>
    {

    }
}
