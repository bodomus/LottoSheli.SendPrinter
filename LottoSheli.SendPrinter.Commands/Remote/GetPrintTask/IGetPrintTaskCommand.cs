using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Remote
{
    using Base;
    using GetPrintTask;
    using Entity;

    /// <summary>
    /// Sends <see cref="TicketPayloadData"/> to remote storage by specified <see cref="GetPrintTaskCommandData"/>
    /// </summary>
    public interface IGetPrintTaskCommand : IParametrizedWithResultCommand<GetPrintTaskCommandData, Task<GetPrintTaskResult>>
    {

    }
}
