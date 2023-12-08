using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core.Monitoring
{
    public interface ILoggerPerformanceRecorder : IPerformanceRecorder 
    { }
    public class LoggerPerformanceRecorder : ILoggerPerformanceRecorder
    {
        private readonly ILogger<LoggerPerformanceRecorder> _logger;
        public LoggerPerformanceRecorder(ILogger<LoggerPerformanceRecorder> logger) 
        { 
            _logger = logger;
        }
        public void Record(PerformanceRecord record)
        {
            try
            {
                _logger.LogInformation(record.ToString());
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, string.Empty);
            }
        }

        public Task RecordAsync(PerformanceRecord record)
        {
            return Task.Run(() => Record(record));
        }
    }
}
