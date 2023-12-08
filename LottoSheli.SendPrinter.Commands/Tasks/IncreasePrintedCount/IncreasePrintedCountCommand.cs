using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Increases <see cref="TicketTask.PrintedCount"/> by specified <see cref="IncreasePrintedCountCommandData"/>
    /// </summary>
    [Command(Basic = typeof(IIncreasePrintedCountCommand))]
    public class IncreasePrintedCountCommand : IIncreasePrintedCountCommand
    {
        private readonly ILogger<IncreasePrintedCountCommand> _logger;
        private readonly ITicketTaskRepository _ticketTaskRepository;

        public IncreasePrintedCountCommand(ITicketTaskRepository ticketTaskRepository,
            ILogger<IncreasePrintedCountCommand> logger)
        {
            _logger = logger;
            _ticketTaskRepository = ticketTaskRepository;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute(IncreasePrintedCountCommandData data)
        {
            data.ExistsingTicketTask.PrintedCount++;
            _ticketTaskRepository.Update(data.ExistsingTicketTask);
            
        }
    }
}
