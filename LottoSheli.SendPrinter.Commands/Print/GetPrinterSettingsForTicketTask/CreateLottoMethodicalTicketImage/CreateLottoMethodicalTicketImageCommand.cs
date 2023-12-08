using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates regular methodical lotto ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<CreatedLottoMethodicalTicketImageResult>))]
    public class CreateLottoMethodicalTicketImageCommand : CreateLottoMethodicalTicketImageBase<CreatedLottoMethodicalTicketImageResult>, ICreateLottoTicketImageCommand<CreatedLottoMethodicalTicketImageResult>
    {
        private readonly ILogger<CreateLottoMethodicalTicketImageCommand> _logger;

        public CreateLottoMethodicalTicketImageCommand(ITicketRenderer ticketRenderer, ILogger<CreateLottoMethodicalTicketImageCommand> logger)
            :base(ticketRenderer, logger)
        {
            _logger = logger;
        }

        public override CreatedLottoMethodicalTicketImageResult Execute(CreateLottoTicketImageCommandData data)
        {
            return new CreatedLottoMethodicalTicketImageResult
            { 
                Image = RenderTicket( data, "LottoRegularMethodical") 
            };
        }
    }
}
