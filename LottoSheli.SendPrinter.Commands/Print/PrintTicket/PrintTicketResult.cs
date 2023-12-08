using LottoSheli.SendPrinter.Printer.Exceptions;
using System;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class PrintTicketResult
    {
        public int Status { get; init; }

        public int PrintedCountLeft { get; init; }

        public PrintingException Error;
    }
}
