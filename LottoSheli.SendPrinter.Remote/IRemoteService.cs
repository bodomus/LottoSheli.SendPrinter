using System.Threading.Tasks;

using LottoSheli.SendPrinter.DTO.Remote;

namespace LottoSheli.SendPrinter.Remote
{
    public interface IRemoteService : IConnectionStateInformer
    {
        Task RefreshConnection();

        Task<bool> TestConnection(string d9ServerUrl, string accessKey);

        Task<SummaryReportResponseData> CreateSummaryReport(SummaryReportData summaryReportData);

        Task<bool> UpdateTicketDate(TicketData ticketData);

        //Task<bool> SendRecognitionMismatch(RecognizeMismatch ticketData);
    }
}
