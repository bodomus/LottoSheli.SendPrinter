using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.OCR
{

    /// <summary>
    /// Subscribes to <see cref="Draw"/> collection state changes.
    /// </summary>
    [Command(Basic = typeof(ISubscribeEntityStateCommand<Draw>))]
    public class SubscribeDrawStateCommand : SubscribeEntityStateCommand<Draw, IDrawRepository>, ISubscribeEntityStateCommand<Draw>
    {

        public SubscribeDrawStateCommand(ILogger<SubscribeTicketTaskStateCommand> logger, IDrawRepository drawRepository)
            :base(logger, drawRepository)
        {
        }
    }
}
