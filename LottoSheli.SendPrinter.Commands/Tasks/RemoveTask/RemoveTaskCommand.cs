using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Removes <see cref="TicketTask"/> by specified <see cref="RemoveTaskCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IRemoveTaskCommand))]
    public class RemoveTaskCommand : IRemoveTaskCommand
    {
        private readonly ILogger<RemoveTaskCommand> _logger;
        private readonly ITicketTaskRepository _ticketTaskRepository;
        private readonly ISequenceService _sequenceService;

        public RemoveTaskCommand(ITicketTaskRepository ticketTaskRepository, ISequenceService sequenceService, ILogger<RemoveTaskCommand> logger)
        {
            _logger = logger;
            _sequenceService = sequenceService;
            _ticketTaskRepository = ticketTaskRepository;
        }

        public bool CanExecute()
        {
            return true;
        }

        public bool Execute(RemoveTaskCommandData data)
        {
            bool removed = _ticketTaskRepository.Remove(data.Id);
            if (removed && 0 == _ticketTaskRepository.Count())
                _sequenceService.Reset();
            return removed;
        }
    }
}
