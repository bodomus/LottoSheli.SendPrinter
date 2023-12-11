using LottoSheli.SendPrinter.DTO.Remote;
using LottoSheli.SendPrinter.OCR.Google;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    public interface IRemoteService : IConnectionStateInformer
    {
        Task RefreshConnection();

        Task<bool> TestConnection(string d9ServerUrl, string accessKey);

        Task<SummaryReportResponseData> CreateSummaryReport(SummaryReportData summaryReportData);

        Task<bool> UpdateTicketDate(TicketData ticketData);

        Task<bool> SendRecognitionMismatch(RecognizeMismatch ticketData);
    }
}
