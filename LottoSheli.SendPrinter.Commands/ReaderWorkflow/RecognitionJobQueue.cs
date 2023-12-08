using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow
{
    public class RecognitionJobQueue : AsyncQueue<RecognitionJobWorker>, IRecognitionJobQueue
    {
        private ILogger<RecognitionJobQueue> _logger;
        public event EventHandler<RecognitionJobWorker> JobEnqueued;
        public event EventHandler<RecognitionJobWorker> JobStarted;
        public event EventHandler<RecognitionJobWorker> JobFinished;
        public event EventHandler QueueEmpty;

        public RecognitionJobQueue(ISettings settings, ILogger<RecognitionJobQueue> logger) : base(1) 
        {
            _logger = logger;
        }
        public override void Enqueue(RecognitionJobWorker jobWorker)
        {
            base.Enqueue(jobWorker);
            JobEnqueued?.Invoke(this, jobWorker);
        }
        protected override void DoWork(RecognitionJobWorker jobWorker)
        {
            JobStarted?.Invoke(this, jobWorker);
            try
            {
                jobWorker.Execute();
            }
            catch (Exception ex) 
            {
                _logger.LogError($"RJWorker: exception while running job. {ex.Message}\r\n{ex.StackTrace}");
            }
            finally
            {
                JobFinished?.Invoke(this, jobWorker);
                if (0 == Count && 0 == ActiveWorkers)
                    QueueEmpty?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
