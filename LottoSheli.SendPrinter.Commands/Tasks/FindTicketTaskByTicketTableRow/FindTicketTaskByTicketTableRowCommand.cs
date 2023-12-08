using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Finds <see cref="IEnumerable{TicketTask}"/> by specified <see cref="FindTicketTaskByTicketTableRowCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IFindTicketTaskByTicketTableRowCommand))]
    public class FindTicketTaskByTicketTableRowCommand : IFindTicketTaskByTicketTableRowCommand
    {
        private readonly ILogger<FindTicketTaskByTicketTableRowCommand> _logger;
        private readonly ITicketTaskRepository _ticketTaskRepository;

        public FindTicketTaskByTicketTableRowCommand(ITicketTaskRepository ticketTaskRepository, ILogger<FindTicketTaskByTicketTableRowCommand> logger)
        {
            _logger = logger;
            _ticketTaskRepository = ticketTaskRepository;
        }

        public bool CanExecute()
        {
            return true;
        }

        public IEnumerable<TicketTask> Execute(FindTicketTaskByTicketTableRowCommandData data)
        {
            return _ticketTaskRepository.FindByTicketTableRow(data.SingleRow);
        }
    }
}
