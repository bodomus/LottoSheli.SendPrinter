using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands
{
    /// <summary>
    /// Command interface for DI
    /// </summary>
    public interface IMatchCommand : IRecognitionJobCommand
    {
    }

    /// <summary>
    /// Recognizes scanned ticket using SlipReader module
    /// </summary>
    [Command(Basic = typeof(IMatchCommand))]
    public class MatchCommand : RecognitionJobCommand, IMatchCommand
    {
        private ITicketTaskRepository _ticketTaskRepository;
        

        public MatchCommand(ITicketTaskRepository ticketTaskRepository, ILogger<IRecognitionJobCommand> logger) 
            : this(Guid.NewGuid().ToString(), ticketTaskRepository, logger)
        {
        }

        private MatchCommand(string guid, ITicketTaskRepository ticketTaskRepository, ILogger<IRecognitionJobCommand> logger)
        {
            _jobGuid = guid;
            _logger = logger;
            _ticketTaskRepository = ticketTaskRepository;
        }

        public override IRecognitionJobCommand CreateNew(string guid)
        {
            return new MatchCommand(guid, _ticketTaskRepository, _logger);
        }

        protected override TrenaryResult ExecuteInternally(RecognitionJob job)
        {
            IEnumerable<TicketTask> candidateTickets = _ticketTaskRepository.GetAll();
            TicketMatchingError bestMatchingResult = TicketMatchingError.Unmatched;
            TicketTask bestMatchingTicket = null;
            foreach (var ticket in candidateTickets)
            {
                TicketMatchingError match = TicketTaskMatcher.Match(job.RecognizedData, ticket);
                if (match < bestMatchingResult) 
                { 
                    bestMatchingResult = match;
                    bestMatchingTicket = ticket;
                }
                if (TicketMatchingError.None == match) 
                {
                    job.Ticket = ticket;
                    job.MatchStatus = match;
                    return TrenaryResult.Done;
                }
            }
            if (bestMatchingResult <= TicketMatchingError.Price) 
            {
                job.Ticket = bestMatchingTicket;
                job.MatchStatus = bestMatchingResult;
                return TrenaryResult.Partial;
            }
            return TrenaryResult.Failed;
        }
    }
}
