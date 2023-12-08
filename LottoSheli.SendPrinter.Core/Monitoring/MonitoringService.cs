using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core.Monitoring
{
    public class MonitoringService : IMonitoringService
    {
        private const string CATEGORY = "Lotto.SendPrinter.App";
        private const int ARD_MAX_SAMPLES = 20;
        private readonly List<int> _ardSamples = new List<int>();
        
        private PerformanceCounter _ardProbe;
        private PerformanceCounter _ardBase;
        private PerformanceCounter _printedProbe;
        private PerformanceCounter _sentProbe;
        private PerformanceCounter _cpuProbe;
        private PerformanceCounter _memoryProbe;
        private PerformanceCounter _handleProbe;

        private object _lock = new object();
        private bool _probesCreated => null != _ardProbe 
            && null != _printedProbe 
            && null != _sentProbe;
        public MonitoringService() 
        {
            SetupCategory();
        }
        public async Task<PerformanceRecord> GetPerformanceRecord()
        {
            await EnsureProbesCreated();

            return new PerformanceRecord
            {
                AverageRequestDuration = _ardSamples.Count > 0 ? (int)Math.Round(_ardSamples.Average()) : 0,
                TicketsPrinted = (int)_printedProbe.NextValue(),
                TicketsSent = (int)_sentProbe.NextValue(),
                CPUUsage = _cpuProbe.NextValue(),
                HandlesUsed = (int)_handleProbe.NextValue(),
                MemoryUsage = (long)_memoryProbe.NextValue(),
            };
        }

        public async Task InformRequestDuration(int duration)
        {
            await EnsureProbesCreated();
            _ardProbe.IncrementBy(duration);
            _ardBase.Increment();
            lock(_lock) 
            {
                _ardSamples.Add(duration);
                while(_ardSamples.Count > ARD_MAX_SAMPLES)
                    _ardSamples.RemoveAt(0);
            }
        }

        public async Task InformTicketPrinted()
        {
            await EnsureProbesCreated();
            _printedProbe.Increment();
        }

        public async Task InformTicketSent()
        {
            await EnsureProbesCreated();
            _sentProbe.Increment();
        }

        private bool SetupCategory()
        {
            if (!PerformanceCounterCategory.Exists(CATEGORY))
            {

                CounterCreationDataCollection counterDataCollection = new CounterCreationDataCollection();

                CounterCreationData averageReqDuration = new CounterCreationData();
                averageReqDuration.CounterType = PerformanceCounterType.AverageCount64;
                averageReqDuration.CounterName = "ARD";
                counterDataCollection.Add(averageReqDuration);

                CounterCreationData avgTimerBase = new CounterCreationData();
                avgTimerBase.CounterType = PerformanceCounterType.AverageBase;
                avgTimerBase.CounterName = "AverageBase";
                counterDataCollection.Add(avgTimerBase);

                CounterCreationData printedCount = new CounterCreationData();
                printedCount.CounterType = PerformanceCounterType.CounterDelta32;
                printedCount.CounterName = "TicketsPrintedCount";
                counterDataCollection.Add(printedCount);

                CounterCreationData sentCount = new CounterCreationData();
                sentCount.CounterType = PerformanceCounterType.CounterDelta32;
                sentCount.CounterName = "TicketsSentCount";
                counterDataCollection.Add(sentCount);

                PerformanceCounterCategory.Create(CATEGORY,
                    "Represents internal app set of counters",
                    PerformanceCounterCategoryType.SingleInstance, counterDataCollection);

                return (true);
            }
            else
            {
                Console.WriteLine($"Category exists - {CATEGORY}");
                return (false);
            }
        }

        private async Task EnsureProbesCreated() 
        {
            if (!_probesCreated) 
            {
                CreateProbes();
                await Task.Delay(100);
            }
        }

        private void CreateProbes() 
        {
            _ardProbe = new PerformanceCounter(CATEGORY, "ARD", false);
            _ardProbe.RawValue = 0;
            _ardBase = new PerformanceCounter(CATEGORY, "AverageBase", false);
            _ardBase.RawValue = 0;
            _printedProbe = new PerformanceCounter(CATEGORY, "TicketsPrintedCount", false);
            _printedProbe.RawValue = 0;
            _sentProbe = new PerformanceCounter(CATEGORY, "TicketsSentCount", false);
            _sentProbe.RawValue = 0;
            _cpuProbe = new PerformanceCounter("Process", "% Processor Time", true);
            _cpuProbe.InstanceName = "LottoSheli.SendPrinter.App";
            _memoryProbe = new PerformanceCounter("Process", "Private Bytes", true);
            _memoryProbe.InstanceName = "LottoSheli.SendPrinter.App";
            _handleProbe = new PerformanceCounter("Process", "Handle Count", true);
            _handleProbe.InstanceName = "LottoSheli.SendPrinter.App";
        }
    }
}
