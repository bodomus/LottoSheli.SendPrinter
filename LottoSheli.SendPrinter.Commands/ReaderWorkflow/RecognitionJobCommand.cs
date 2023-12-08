using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands
{
    public abstract class RecognitionJobCommand : IRecognitionJobCommand
    {
        protected ILogger<IRecognitionJobCommand> _logger;
        protected string _jobGuid;

        public event EventHandler<RecognitionJobEventArgs> Started;
        public event EventHandler<RecognitionJobEventArgs> Completed;

        public abstract IRecognitionJobCommand CreateNew(string guid);

        public virtual TrenaryResult Execute(RecognitionJob job) 
        { 
            var eventArgs = new RecognitionJobEventArgs { Job = job };
            Started?.Invoke(this, eventArgs);
            try
            {
                return ExecuteInternally(job);
            }
            catch(Exception ex)
            {
                _logger?.LogError($"Failed to execute command {GetType().Name} due to error {ex.Message}");
                return TrenaryResult.Failed;
            }
            finally 
            { 
                Completed?.Invoke(this, eventArgs);
            }
        }

        protected abstract TrenaryResult ExecuteInternally(RecognitionJob job);

        
    }
}
