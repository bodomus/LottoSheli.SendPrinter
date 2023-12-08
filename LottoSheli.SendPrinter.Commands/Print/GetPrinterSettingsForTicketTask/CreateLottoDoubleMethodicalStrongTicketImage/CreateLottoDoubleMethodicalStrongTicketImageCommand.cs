using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates double methodical lotto ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<CreatedLottoDoubleMethodicalStrongTicketImageResult>))]
    public class CreateLottoDoubleMethodicalStrongTicketImageCommand : 
        CreateLottoMethodicalStrongTicketImageBase<CreatedLottoDoubleMethodicalStrongTicketImageResult>, ICreateLottoTicketImageCommand<CreatedLottoDoubleMethodicalStrongTicketImageResult>
    {
        private readonly ILogger<CreateLottoDoubleMethodicalStrongTicketImageCommand> _logger;

        public CreateLottoDoubleMethodicalStrongTicketImageCommand(ITicketRenderer ticketRenderer, ILogger<CreateLottoDoubleMethodicalStrongTicketImageCommand> logger)
            :base(ticketRenderer, logger)
        {
            _logger = logger;
        }

        public override CreatedLottoDoubleMethodicalStrongTicketImageResult Execute(CreateLottoTicketImageCommandData data)
        {
            return new CreatedLottoDoubleMethodicalStrongTicketImageResult
            { 
                Image = RenderTicket( data, "LottoDoubleMethodicalStrong") 
            };
        }
    }
}
