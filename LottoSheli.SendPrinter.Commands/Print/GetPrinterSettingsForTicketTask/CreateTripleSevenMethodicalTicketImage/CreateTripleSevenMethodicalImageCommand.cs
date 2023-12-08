using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates 777 methodical ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<CreatedTripleSevenMethodicalTicketImageResult>))]
    public class CreateTripleSevenMethodicalTicketImageCommand : ICreateLottoTicketImageCommand<CreatedTripleSevenMethodicalTicketImageResult>
    {
        private readonly ILogger<CreateTripleSevenMethodicalTicketImageCommand> _logger;
        private readonly ITicketRenderer _ticketRenderer;

        public CreateTripleSevenMethodicalTicketImageCommand(ITicketRenderer ticketRenderer, ILogger<CreateTripleSevenMethodicalTicketImageCommand> logger)
        {
            _logger = logger;
            _ticketRenderer = ticketRenderer;
        }

        public bool CanExecute()
        {
            return true;
        }

        public CreatedTripleSevenMethodicalTicketImageResult Execute(CreateLottoTicketImageCommandData data)
        {

            // Build element indices
            var indices = new Dictionary<string, List<int>>();

            data.FillCommonElements(indices);

            dynamic methodicalValue = data.ExisintgTicketTask.Settings["methodical"];
            int index = methodicalValue switch
            {
                8 => 0,
                9 => 1,
                _ => throw new NotSupportedException("Unexpected methodical type")
            };

            indices.Add("form_type", new List<int> { index });

            return new CreatedTripleSevenMethodicalTicketImageResult { Image = _ticketRenderer.RenderTicket("777Methodical", indices) };
        }

    }
}
