using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core.Monitoring
{
    public interface IMonitoringService
    {
        Task InformRequestDuration(int duration);
        Task InformTicketPrinted();
        Task InformTicketSent();

        Task<PerformanceRecord> GetPerformanceRecord();
    }
}
