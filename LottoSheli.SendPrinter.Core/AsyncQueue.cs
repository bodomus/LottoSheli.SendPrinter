using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core
{
    public abstract class AsyncQueue<TJob> : IDisposable
    {
        private const int DEFAULT_CONCURRENCY = 2;

        protected int _concurrency = DEFAULT_CONCURRENCY;
        
        protected SemaphoreSlim _throttler = new SemaphoreSlim(DEFAULT_CONCURRENCY);
        protected CancellationTokenSource _ctsrc = new CancellationTokenSource();

        protected readonly ConcurrentQueue<TJob> _queue = new ConcurrentQueue<TJob>();

        public int Count => _queue.Count;
        public int ActiveWorkers => _concurrency - _throttler.CurrentCount;
        public IEnumerable<TJob> AsEnumerable => _queue.AsEnumerable();

        public AsyncQueue() { }
        
        public AsyncQueue(int threads) : this()
        {
            _concurrency = threads;
            _throttler = new SemaphoreSlim(threads);
        }

        public virtual void Enqueue(TJob job) 
        {
            _queue.Enqueue(job);
            if (_throttler.CurrentCount > 0)
            {
                _ = EngageWorker();
            }
        }

        public virtual void Clear() 
        { 
            _ctsrc.Cancel();
            _ctsrc.Dispose();
            _ctsrc = new CancellationTokenSource();
            _queue.Clear();
        }

        protected virtual Task EngageWorker() 
        {
            var ct = _ctsrc.Token;
            return Task.Factory.StartNew(() =>
            {
                _throttler.Wait();
                while (_queue.TryDequeue(out TJob job))
                {
                    try
                    {
                        ct.ThrowIfCancellationRequested();
                        DoWork(job);
                    }
                    catch (OperationCanceledException) { break; }
                    catch { throw; }
                    finally { _throttler.Release(); }
                }
            }, ct, TaskCreationOptions.PreferFairness | TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }
        protected abstract void DoWork(TJob job);

        public virtual void Dispose()
        {
            if (null != _ctsrc)
            {
                _ctsrc.Cancel();
                _ctsrc.Dispose();
            }
        }
    }
}
