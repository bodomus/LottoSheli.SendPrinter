using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    public interface ISendingQueue
    {
        /// <summary>
        /// Event is fired when a job is added to the queue
        /// </summary>
        event EventHandler<SendingQueueEventArgs> JobEnqueuedForSending;
        /// <summary>
        /// Event is fired when a job is taken from the queue and actual sending started
        /// </summary>
        event EventHandler<SendingQueueEventArgs> JobSendingStarted;
        /// <summary>
        /// Event is fired when sending finished irrespectively to results
        /// </summary>
        event EventHandler<SendingQueueEventArgs> JobSendingFinished;

        /// <summary>
        /// Get sending queue running state
        /// </summary>
        /// <returns>True if sending queue is idle</returns>
        bool IsStopped { get; }

        /// <summary>
        /// Get the number of active senders at the moment
        /// </summary>
        /// <returns>number of concurrent workers</returns>
        int ActiveWorkerCount { get; }

        /// <summary>
        /// Calculates sum of sending and pending jobs
        /// </summary>
        /// <returns>total number of jobs</returns>
        int TotalJobCount { get; }

        // <summary>
        /// Calculates average sending duration from samples
        /// </summary>
        /// <returns>average sending duration in milliseconds</returns>

        public int AverageSendingDuration { get; }

        IEnumerable<RecognitionJob> PendingJobs { get; }
        IEnumerable<RecognitionJob> SendingJobs { get; }

        /// <summary>
        /// Loads  stored jobs having Sending status to the queue and starts
        /// configured amount of workers
        /// </summary>
        void Start();

        /// <summary>
        /// Stops executing all running jobs and clears the queue.
        /// However DOES NOT change Sending status to NotSent thus allowing
        /// for jobs to be enqueued on SendingQueue restart
        /// </summary>
        void Stop();

        /// <summary>
        /// Enqueues job for sending
        /// </summary>
        /// <param name="job">Recognition Job <see cref="RecognitionJob"/> to be enqueued for sending</param>
        void StartJob(RecognitionJob job);

        /// <summary>
        /// Loads  stored jobs having NotSent status to the queue.
        /// </summary>
        void StartAllJobs();

        /// <summary>
        /// Removes job from sending queue
        /// </summary>
        /// <param name="job">Enqueued job <see cref="RecognitionJob"/> to be removed from sending queue</param>
        void StopJob(RecognitionJob job);

        /// <summary>
        /// Stops all currently Sending jobs changing their status to NotSent.
        /// Unlike Stop() method which doesn't change job statuses StopAllJobs() 
        /// will set status to NotSent which will prevent job from sending after restart
        /// </summary>
        void StopAllJobs();
    }
}
