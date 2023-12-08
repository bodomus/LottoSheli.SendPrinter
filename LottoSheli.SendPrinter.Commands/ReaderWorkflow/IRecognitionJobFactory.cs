using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow
{
    public interface IRecognitionJobFactory
    {
        IRecognitionJobRepository Repository { get; }
        RecognitionJob CreateJob(Bitmap scan, int scanType);
        IRecognitionJobWorker CreateJobWorker(string guid);
        IRecognitionJobWorker CreateJobWorker(Bitmap scan, int scanType);
        IRecognitionJobWorker CreateJobWorker(RecognitionJob job);
        IRecognitionJobWorker GetJobWorker(int jobId);
        IEnumerable<IRecognitionJobWorker> GetStoredWorkers();
        IEnumerable<RecognitionJob> GetStoredJobs();
    }
}
