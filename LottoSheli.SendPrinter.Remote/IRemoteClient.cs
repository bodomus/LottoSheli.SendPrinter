using System;
using System.IO;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    using Entity;
    using Entity.Enums;
    using LottoSheli.SendPrinter.DTO.Remote;
    using System.Net.Http;

    public interface IRemoteClient
    {
        SessionInfo GetSession(Role role);
        SessionManager GetSessionManager(Role role);
        Task Reconnect(Role role);
        
        Task<string> GetDrawsAsync();

        Task<string> GetWinners(int did, int stationId);

        Task<string> GetPrintTask(TicketFilter filter, int limit);

        Task SendPrintResultAsync(int id, PrintTaskResult payload);

        Task<SummaryReportResponseData> CreateSummaryReport(SummaryReportData summaryReportData);

        Task<bool> UpdateSummaryReport(TicketData ticketData);

        Task<bool> SendScanResultAsync(TicketPayloadData payload, Stream stream);

        Task<string> CheckTicketAsync(string slipId);

        void CancelPendingRequests();
    }
}