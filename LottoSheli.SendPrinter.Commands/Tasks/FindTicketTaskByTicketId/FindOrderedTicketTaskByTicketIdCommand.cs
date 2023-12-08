using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    /// <summary>
    /// Finds <see cref="TicketTask"/> by specified <see cref="FindTicketTaskByTicketIdCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IFindOrderedTicketTaskByTicketIdCommand))]
    public class FindOrderedTicketTaskByTicketIdCommand : IFindOrderedTicketTaskByTicketIdCommand
    {
        private readonly ILogger<FindOrderedTicketTaskByTicketIdCommand> _logger;
        private readonly ITicketTaskRepository _ticketTaskRepository;

        public FindOrderedTicketTaskByTicketIdCommand(ITicketTaskRepository ticketTaskRepository, ILogger<FindOrderedTicketTaskByTicketIdCommand> logger)
        {
            _logger = logger;
            _ticketTaskRepository = ticketTaskRepository;
        }

        public bool CanExecute()
        {
            return true;
        }

        public TicketTask Execute(FindTicketTaskByTicketIdCommandData data)
        {
            return _ticketTaskRepository.FindByTicketId(data.TicketId);
        }
    }
}
