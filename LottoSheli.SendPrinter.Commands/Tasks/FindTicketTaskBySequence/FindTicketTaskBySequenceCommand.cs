using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Finds <see cref="TicketTask"/> by specified <see cref="FindTicketTaskBySequenceCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IFindTicketTaskBySequenceCommand))]
    public class FindTicketTaskBySequenceCommand : IFindTicketTaskBySequenceCommand
    {
        private readonly ILogger<FindTicketTaskBySequenceCommand> _logger;
        private readonly ITicketTaskRepository _ticketTaskRepository;

        public FindTicketTaskBySequenceCommand(ITicketTaskRepository ticketTaskRepository, ILogger<FindTicketTaskBySequenceCommand> logger)
        {
            _logger = logger;
            _ticketTaskRepository = ticketTaskRepository;
        }

        public bool CanExecute()
        {
            return true;
        }

        public TicketTask Execute(FindTicketTaskBySequenceCommandData data)
        {
            var result = _ticketTaskRepository.FindBySequence(data.SeqenceNumber);
            return result;
        }
    }
}
