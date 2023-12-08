using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print;
using LottoSheli.SendPrinter.Commands.Tasks;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Remote;
using LottoSheli.SendPrinter.Repository.Converters;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Remote
{
    /// <summary>
    /// Get ticket for printing
    /// </summary>
    [Command(Basic = typeof(ISendPrintResultCommand))]
    public class SendPrintResultCommand : ISendPrintResultCommand
    {
        private readonly ILogger<SendPrintResultCommand> _logger;
        private readonly IRemoteClient _remoteClient;
        private readonly ITicketTaskConverter _ticketTaskConverter;
        private readonly IAddTicketTaskCommand _addTicketTaskCommand;
        private readonly ICheckTaskPrintedCommand _checkTaskPrintedCommand;

        public SendPrintResultCommand(ILogger<SendPrintResultCommand> logger, IRemoteClient remoteClient, ITicketTaskConverter ticketTaskConverter,
            IAddTicketTaskCommand addTicketTaskCommand, ICheckTaskPrintedCommand checkTaskPrintedCommand)
        {
            _logger = logger;
            _remoteClient = remoteClient;
            _ticketTaskConverter = ticketTaskConverter;
            _addTicketTaskCommand = addTicketTaskCommand;
            _checkTaskPrintedCommand = checkTaskPrintedCommand;
        }

        private PrintTaskResult SerializePrintResponse(TicketTask task)
        {
            var response = new PrintTaskResult();
            response.Data.QueueName = task.QueueName;
            response.Data.Status = 1;
            return response;
        }

        public async Task Execute(SendPrintResultCommandData data)
        {
            var result = SerializePrintResponse(data.NewTask);
            await _remoteClient.SendPrintResultAsync(data.NewTask.Id, result);
            var addedTask = _addTicketTaskCommand.Execute(new AddTicketTaskCommandData { NewTicketTask = data.NewTask });

            _logger.LogInformation("Added task (ID: {0}, TID: {1}) to scan queue (sequence: {2})", addedTask.Id, addedTask.TicketId, addedTask.Sequence);
        }

        public bool CanExecute()
        {
            return true;
        }

        private async Task DoSendingInLoop(SendPrintResultCommandData data) 
        { 
            try 
            { 
                var result = SerializePrintResponse(data.NewTask);
                await _remoteClient.SendPrintResultAsync(data.NewTask.Id, result);
            }
            catch 
            { 
                await Task.Delay(1000);
                await DoSendingInLoop(data);
            }
        }
    }
}
