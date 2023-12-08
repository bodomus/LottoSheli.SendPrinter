using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    /// <summary>
    /// Resets bundle numbers for all <see cref="TicketTask"/>s"/>
    /// </summary>
    [Command(Basic = typeof(IUpdateTicketBundlesCommand))]
    public class UpdateTicketBundlesCommand : IUpdateTicketBundlesCommand
    {
        private ITicketTaskRepository _ticketTaskRepository;
        public UpdateTicketBundlesCommand(ITicketTaskRepository ticketTaskRepository) 
        { 
            _ticketTaskRepository = ticketTaskRepository;
        }
        public bool CanExecute()
        {
            return true;
        }

        public void Execute(UpdateTicketBundlesCommandData data)
        {
            var tickets = _ticketTaskRepository.GetAll().OrderBy(t => t.Sequence).ToList();
            if (0 == tickets.Count)
                return;

            int bundle = tickets.Max(t => t.Bundle) + 1;
            int firstSeq = tickets[0].Sequence;
            int lastSeq = tickets[tickets.Count - 1].Sequence;
            
            if (null == data.Ranges)
                data.Ranges = new List<Tuple<int, int>>();
            if (0 == data.Ranges.Count)
            {
                bundle = -1;
                data.Ranges.Add(new Tuple<int, int>(firstSeq, lastSeq));
            }
            foreach (var range in data.Ranges) 
            {
                if (range is { Item1: var start, Item2: var end } && start <= lastSeq && end >= firstSeq) 
                {
                    if (start > end)
                        (start, end) = (end, start);
                    
                    foreach (var ticket in tickets.Where(t => t.Sequence >= start && t.Sequence <= end)) 
                    { 
                        ticket.Bundle = bundle;
                        _ticketTaskRepository.Update(ticket);
                    }
                    bundle++;
                }
            }
        }
    }
}
