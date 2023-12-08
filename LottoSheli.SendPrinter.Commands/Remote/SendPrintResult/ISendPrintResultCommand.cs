using LottoSheli.SendPrinter.Commands.Base;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Remote
{
    /// <summary>
    /// Sends print payload to remote storage by specified <see cref="SendPrintResultCommandData"/>
    /// </summary>
    public interface ISendPrintResultCommand : IParametrizedWithResultCommand<SendPrintResultCommandData, Task>
    {

    }
}
