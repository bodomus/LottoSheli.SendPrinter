using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.OCR
{

    /// <summary>
    /// Subscribes to <see cref="TicketTask"/> collection state changes.
    /// </summary>
    [Command(Basic = typeof(ISubscribeEntityStateCommand<TicketTask>))]
    public class SubscribeTicketTaskStateCommand : SubscribeEntityStateCommand<TicketTask, ITicketTaskRepository>, ISubscribeEntityStateCommand<TicketTask>
    {

        public SubscribeTicketTaskStateCommand(ILogger<SubscribeTicketTaskStateCommand> logger, ITicketTaskRepository ticketTaskRepository)
            :base(logger, ticketTaskRepository)
        {
        }
    }
}
