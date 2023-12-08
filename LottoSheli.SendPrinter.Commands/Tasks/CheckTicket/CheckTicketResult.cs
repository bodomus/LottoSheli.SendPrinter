using LottoSheli.SendPrinter.OCR;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    public class CheckTicketResult
    {
        public long TicketId { get; set; }

        public string ReceivedTicketUrl { get; init; }

        /// <summary>
        /// Recognition error result
        /// </summary>
        public OcrException RecognitionError { get; init; }
    }
}
