using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core.Monitoring
{
    public class PerformanceRecord
    {
        public int AverageRequestDuration { get; set; } = 300;
        public int TicketsPrinted { get; set; } = 0;
        public int TicketsSent { get; set; } = 0;
        public double CPUUsage { get; set; } = 0.0;
        public long MemoryUsage { get; set; } = 0L;
        public int HandlesUsed { get; set; } = 0;

        public override string ToString()
        {
            return $"{CPUUsage.ToString("F2")};{MemoryUsage};{HandlesUsed};{AverageRequestDuration};{TicketsPrinted};{TicketsSent}";
        }

        public static PerformanceRecord Empty => new PerformanceRecord();
    }
}
