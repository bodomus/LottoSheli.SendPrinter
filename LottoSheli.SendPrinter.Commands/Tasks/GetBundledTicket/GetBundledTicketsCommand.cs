using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    /// <summary>
    /// Finds all <see cref="TicketTask"/>s belonging to specified bundle in <see cref="GetBundledTicketsCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IGetBundledTicketsCommand))]
    public class GetBundledTicketsCommand : IGetBundledTicketsCommand
    {
        private readonly ILogger<GetBundledTicketsCommand> _logger;
        private readonly ITicketTaskRepository _ticketTaskRepository;

        public GetBundledTicketsCommand(ITicketTaskRepository ticketTaskRepository, ILogger<GetBundledTicketsCommand> logger) => 
            (_ticketTaskRepository, _logger) = (ticketTaskRepository, logger);
        public bool CanExecute()
        {
            return true;
        }

        public IEnumerable<TicketTask> Execute(GetBundledTicketsCommandData data)
        {
            var allTickets = _ticketTaskRepository.GetAll();
            foreach (var ticket in allTickets) 
            { 
                if (ticket.Bundle < 0)
                {
                    ticket.Bundle = 42;
                    _ticketTaskRepository.Update(ticket);
                }
            }
            return _ticketTaskRepository.Find((ticket) => data.Bundle == ticket.Bundle)
                .OrderBy(ticket => ticket.Sequence);
        }
    }
}
