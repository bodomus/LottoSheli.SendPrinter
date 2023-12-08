using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.Print.Base
{

    /// <summary>
    /// Prints <see cref="TicketTask"/> by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    public interface ICreateLottoTicketImageCommand<TResult> : IParametrizedWithResultCommand<CreateLottoTicketImageCommandData, TResult>
        where TResult : CreatedImageResultBase, new()
    {

    }
}
