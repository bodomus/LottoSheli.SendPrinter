using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Prints <see cref="TicketTask"/> by specified <see cref="PrintBarcodeCommandData"/>
    /// </summary>
    public interface IPrintBarcodeCommand : IParametrizedWithResultCommand<PrintBarcodeCommandData, Task<string>>
    {

    }
}
