using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Printer.Exceptions;
using System;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Commands.Remote.GetPrintTask
{
    public class GetPrintTaskResult
    {
        public IList<TicketTask> TicketTasks;

        public Exception Error;
    }
}
