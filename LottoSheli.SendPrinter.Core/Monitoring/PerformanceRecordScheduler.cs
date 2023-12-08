using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core.Monitoring
{
    public class PerformanceRecordScheduler : IPerformanceRecordScheduler
    {
        private readonly List<IPerformanceRecorder> _recorders = new List<IPerformanceRecorder>();
        private readonly IMonitoringService _monitoringService;
        private System.Threading.Timer _timer;

        public bool IsRunning => null != _timer;

        public PerformanceRecordScheduler(IMonitoringService monService) 
        { 
            _monitoringService = monService;
        }

        public void AddRecorder(IPerformanceRecorder recorder) 
        {
            if (!_recorders.Contains(recorder))
                _recorders.Add(recorder);
        }

        public async void Execute(object state) 
        {
            var record = await _monitoringService.GetPerformanceRecord();
            foreach (var recorder in _recorders)
            {
                try
                {
                    await recorder.RecordAsync(record);
                }
                catch
                {
                    continue;
                }
            }
        }

        public void Initialize(int interval) 
        {
            if (IsRunning && interval > 0)
                _timer.Change(1000, interval);
            else
                _timer = new System.Threading.Timer(Execute, null, 1000, interval);
        }

    }
}
