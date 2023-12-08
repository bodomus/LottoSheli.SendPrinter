using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core.Monitoring
{
    public interface IPerformanceRecorder
    {
        void Record(PerformanceRecord record);
        Task RecordAsync(PerformanceRecord record);
    }
}
