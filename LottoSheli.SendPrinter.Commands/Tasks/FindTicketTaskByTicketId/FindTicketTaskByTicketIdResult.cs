using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.OCR;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    public class FindTicketTaskByTicketIdResult
    {
        public TicketTask TicketData { get; init; }
    }
}
