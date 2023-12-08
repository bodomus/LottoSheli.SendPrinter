using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Returns max sequence of <see cref="TicketTask"/>
    /// </summary>
    [Command(Basic = typeof(IGetMaxTicketTaskSequenceCommand))]
    public class GetMaxTicketTaskSequenceCommand : IGetMaxTicketTaskSequenceCommand
    {
        private readonly ILogger<AddTicketTaskCommand> _logger;
        private readonly ITicketTaskRepository _ticketTaskRepository;

        public GetMaxTicketTaskSequenceCommand(ITicketTaskRepository ticketTaskRepository, ILogger<AddTicketTaskCommand> logger)
        {
            _logger = logger;
            _ticketTaskRepository = ticketTaskRepository;
        }

        public bool CanExecute()
        {
            return true;
        }

        public int Execute()
        {
            return _ticketTaskRepository.GetMaxSequence();
        }

    }
}
