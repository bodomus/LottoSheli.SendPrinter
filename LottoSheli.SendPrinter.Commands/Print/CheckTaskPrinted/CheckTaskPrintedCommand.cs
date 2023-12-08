using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Checks if specified <see cref="TicketTask"/> is already printed and returns <see cref="DateTime"/> otherwise adds a new log record and returns null.
    /// </summary>
    [Command(Basic = typeof(ICheckTaskPrintedCommand))]
    public class CheckTaskPrintedCommand : ICheckTaskPrintedCommand
    {
        private readonly ILogger<CheckTaskPrintedCommand> _logger;
        private readonly IPrintedTicketLogRepository _printedTicketLogRepository;

        public CheckTaskPrintedCommand(IPrintedTicketLogRepository printedTicketLogRepository, ILogger<CheckTaskPrintedCommand> logger)
        {
            _logger = logger;
            _printedTicketLogRepository = printedTicketLogRepository;
        }

        public bool CanExecute()
        {
            return true;
        }

        public DateTime? Execute(CheckTaskPrintedCommandData data)
        {
            var result = _printedTicketLogRepository.GetById(data.TicketId);

            if (result != null)
                return result.CreatedDate;

            var newLogRecord = _printedTicketLogRepository.CreateNew();

            newLogRecord.Id = data.TicketId;
            newLogRecord.CreatedDate = data.PrintedDate;

            _printedTicketLogRepository.Insert(newLogRecord);

            return null;
        }


    }
}
