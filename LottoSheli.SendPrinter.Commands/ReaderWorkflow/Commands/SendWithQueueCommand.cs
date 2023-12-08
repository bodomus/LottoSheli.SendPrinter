using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Remote;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Remote;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands
{
    /// <summary>
    /// Sends recognition result and matched ticket using legacy IRemoteClient
    /// </summary>
    [Command(Basic = typeof(ISendCommand))]
    public class SendWithQueueCommand : RecognitionJobCommand, ISendCommand
    {
        private readonly ISendingQueue _queue;
        private readonly IUpdateSummaryReportCommand _updateSummaryCommand;
        public SendWithQueueCommand(ISendingQueue queue, IUpdateSummaryReportCommand updateSummaryCommand, ILogger<IRecognitionJobCommand> logger)
            : this(Guid.NewGuid().ToString(), queue, updateSummaryCommand, logger)
        {
        }

        private SendWithQueueCommand(string guid, ISendingQueue queue, IUpdateSummaryReportCommand updateSummaryCommand, ILogger<IRecognitionJobCommand> logger) 
        { 
            _jobGuid = guid;
            _queue = queue;
            _updateSummaryCommand = updateSummaryCommand;
            _logger = logger;
        }
        public override IRecognitionJobCommand CreateNew(string guid)
        {
            return new SendWithQueueCommand(guid, _queue, _updateSummaryCommand, _logger);
        }

        protected override TrenaryResult ExecuteInternally(RecognitionJob job)
        {
            _queue.StartJob(job);
            _ = _updateSummaryCommand.Execute(new UpdateSummaryReportCommandData { Ticket = job.Ticket });
            return TrenaryResult.Partial;
        }
    }
}
