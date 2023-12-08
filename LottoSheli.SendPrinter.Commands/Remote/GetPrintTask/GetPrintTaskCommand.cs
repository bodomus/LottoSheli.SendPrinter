using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print;
using LottoSheli.SendPrinter.Commands.Tasks;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Remote;
using LottoSheli.SendPrinter.Repository.Converters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Remote.GetPrintTask
{
    /// <summary>
    /// Get ticket for printing
    /// </summary>
    [Command(Basic = typeof(IGetPrintTaskCommand))]
    public class GetPrintTaskCommand : IGetPrintTaskCommand
    {
        private readonly ILogger<GetPrintTaskCommand> _logger;
        private readonly IRemoteClient _remoteClient;
        private readonly ITicketTaskConverter _ticketTaskConverter;
        private readonly ICheckTaskPrintedCommand _checkTaskPrintedCommand;


        public GetPrintTaskCommand(ILogger<GetPrintTaskCommand> logger, IRemoteClient remoteClient,
            ITicketTaskConverter ticketTaskConverter, ICheckTaskPrintedCommand checkTaskPrintedCommand)
        {
            _logger = logger;
            _remoteClient = remoteClient;
            _ticketTaskConverter = ticketTaskConverter;
            _checkTaskPrintedCommand = checkTaskPrintedCommand;

        }

        public Task<GetPrintTaskResult> Execute(GetPrintTaskCommandData data)
        {
            return Task.Run(async () =>
            {
                var json = await _remoteClient.GetPrintTask(data.Filter, data.Limit);
                var tickets = _ticketTaskConverter.ConvertMany(json);
                var result = new GetPrintTaskResult { TicketTasks = new List<TicketTask>() };
                var error = new StringBuilder();

                foreach (var ticket in tickets)
                {
                    try
                    {
                        if (data.CheckPrintDuplicates)
                            EnsureTicketNotPrinted(ticket);
                        ticket.CreatedDate = DateTime.Now;
                        result.TicketTasks.Add(ticket);
                    }
                    catch (Exception ex)
                    {
                        error.AppendLine(ex.Message);
                    }
                }

                if (error.Length > 0)
                    result.Error = new Exception(error.ToString());

                return result;
            });
        }

        public bool CanExecute()
        {
            return true;
        }

        private void EnsureTicketNotPrinted(TicketTask ticket)
        {
            var printed = _checkTaskPrintedCommand.Execute(new CheckTaskPrintedCommandData
            { TicketId = ticket.TicketId, PrintedDate = DateTime.Now });

            if (printed.HasValue)
                throw new Exception($"Ticket with TID = {ticket.TicketId} was already printed on {printed}");
        }
    }
}
