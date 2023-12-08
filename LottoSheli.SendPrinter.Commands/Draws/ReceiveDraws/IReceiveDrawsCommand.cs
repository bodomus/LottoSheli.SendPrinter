using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Draws
{

    /// <summary>
    /// Receives <see cref="Draw"/> by specified <see cref="ReceiveDrawsCommandData"/>
    /// </summary>
    public interface IReceiveDrawsCommand : IResultCommand<Task<ReceiveDrawsResult>>
    {

    }
}
