using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Commands.Remote
{
    public class SendPrintResultCommandData : ICommandData
    {
        public TicketTask NewTask { get; set; }
    }
}
