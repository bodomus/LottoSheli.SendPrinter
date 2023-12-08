using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates 777 retgular ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<CreatedTripleSevenTicketImageResult>))]
    public class CreateTripleSevenTicketImageCommand : ICreateLottoTicketImageCommand<CreatedTripleSevenTicketImageResult>
    {
        private readonly ILogger<CreateTripleSevenTicketImageCommand> _logger;
        private readonly ITicketRenderer _ticketRenderer;

        public CreateTripleSevenTicketImageCommand(ITicketRenderer ticketRenderer, ILogger<CreateTripleSevenTicketImageCommand> logger)
        {
            _logger = logger;
            _ticketRenderer = ticketRenderer;
        }

        public bool CanExecute()
        {
            return true;
        }

        public CreatedTripleSevenTicketImageResult Execute(CreateLottoTicketImageCommandData data)
        {
            // Build element indices
            var indices = new Dictionary<string, List<int>>();

            data.FillCommonElements(indices);
            
            return new CreatedTripleSevenTicketImageResult { Image = _ticketRenderer.RenderTicket("777", indices) };
        }

    }
}
