using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates double methodical lotto ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<CreatedLottoDoubleMethodicalTicketImageResult>))]
    public class CreateLottoDoubleMethodicalTicketImageCommand : CreateLottoMethodicalTicketImageBase<CreatedLottoDoubleMethodicalTicketImageResult>, ICreateLottoTicketImageCommand<CreatedLottoDoubleMethodicalTicketImageResult>
    {
        private readonly ILogger<CreateLottoDoubleMethodicalTicketImageCommand> _logger;

        public CreateLottoDoubleMethodicalTicketImageCommand(ITicketRenderer ticketRenderer, ILogger<CreateLottoDoubleMethodicalTicketImageCommand> logger)
            :base(ticketRenderer, logger)
        {
            _logger = logger;
        }

        public override CreatedLottoDoubleMethodicalTicketImageResult Execute(CreateLottoTicketImageCommandData data)
        {
            return new CreatedLottoDoubleMethodicalTicketImageResult
            { 
                Image = RenderTicket( data, "LottoDoubleMethodical") 
            };
        }
    }
}
