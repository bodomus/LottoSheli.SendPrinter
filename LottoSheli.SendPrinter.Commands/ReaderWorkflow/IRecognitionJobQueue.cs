using System;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow
{
    public interface IRecognitionJobQueue
    {
        event EventHandler<RecognitionJobWorker> JobEnqueued;
        event EventHandler<RecognitionJobWorker> JobStarted;
        event EventHandler<RecognitionJobWorker> JobFinished;
        event EventHandler QueueEmpty;
        int Count { get; }
        int ActiveWorkers { get; }
        IEnumerable<RecognitionJobWorker> AsEnumerable { get; }
        void Enqueue(RecognitionJobWorker jobWorker);
        void Clear();
    }
}