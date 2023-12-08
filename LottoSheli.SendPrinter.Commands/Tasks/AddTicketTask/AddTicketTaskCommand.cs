using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Add <see cref="TicketTask"/> by specified <see cref="AddTicketTaskCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IAddTicketTaskCommand))]
    public class AddTicketTaskCommand : IAddTicketTaskCommand
    {
        private readonly ILogger<AddTicketTaskCommand> _logger;
        private readonly ITicketTaskRepository _ticketTaskRepository;
        private readonly IGetMaxTicketTaskSequenceCommand _getMaxTicketTaskSequence;

        public AddTicketTaskCommand(ITicketTaskRepository ticketTaskRepository, IGetMaxTicketTaskSequenceCommand getMaxTicketTaskSequence, ILogger<AddTicketTaskCommand> logger)
        {
            _logger = logger;
            _ticketTaskRepository = ticketTaskRepository;
            _getMaxTicketTaskSequence = getMaxTicketTaskSequence;
        }

        public bool CanExecute()
        {
            return true;
        }

        public TicketTask Execute(AddTicketTaskCommandData data)
        {
            if (data.NewTicketTask is TicketTask ticket)
            {
                if (data.Sequence.HasValue)
                    ticket.Sequence = data.Sequence.Value;

                var storedTicket = _ticketTaskRepository.FindOne(t => t.TicketId == ticket.TicketId);
                return (null == storedTicket)
                    ? _ticketTaskRepository.Insert(data.NewTicketTask)
                    : storedTicket;
            }
            return null;
        }
    }
}
