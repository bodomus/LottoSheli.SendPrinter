using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Tasks.Remote;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Remote
{

    /// <summary>
    /// Resets remote connection
    /// </summary>
    public interface ITestConnectionD9Command : IParametrizedWithResultCommand<TestConnectionCommandData, Task<bool>>
    {

    }
}
