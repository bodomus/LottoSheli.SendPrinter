using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates regular methodical lotto ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<CreatedLottoMethodicalStrongTicketImageResult>))]
    public class CreateLottoMethodicalStrongTicketImageCommand 
        : CreateLottoMethodicalStrongTicketImageBase<CreatedLottoMethodicalStrongTicketImageResult>, ICreateLottoTicketImageCommand<CreatedLottoMethodicalStrongTicketImageResult>
    {
        private readonly ILogger<CreateLottoMethodicalStrongTicketImageCommand> _logger;

        public CreateLottoMethodicalStrongTicketImageCommand(ITicketRenderer ticketRenderer, ILogger<CreateLottoMethodicalStrongTicketImageCommand> logger)
            :base(ticketRenderer, logger)
        {
            _logger = logger;
        }

        public override CreatedLottoMethodicalStrongTicketImageResult Execute(CreateLottoTicketImageCommandData data)
        {
            return new CreatedLottoMethodicalStrongTicketImageResult
            { 
                Image = RenderTicket( data, "LottoRegularMethodicalStrong")
            };
        }
    }
}
