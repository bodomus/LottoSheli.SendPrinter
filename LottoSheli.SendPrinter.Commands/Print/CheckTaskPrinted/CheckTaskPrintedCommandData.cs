using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class CheckTaskPrintedCommandData : ICommandData
    {
        public int TicketId { get; init; }

        public DateTime PrintedDate { get; init; }
    }
}
