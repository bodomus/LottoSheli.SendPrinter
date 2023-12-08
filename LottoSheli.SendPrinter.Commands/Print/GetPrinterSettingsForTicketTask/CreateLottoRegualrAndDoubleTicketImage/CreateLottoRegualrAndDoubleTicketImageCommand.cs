using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates regular and double lotto ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<CreatedLottoRegularAndDoubleImageResult>))]
    public class CreateLottoRegualrAndDoubleTicketImageCommand : CreateLottoTicketImageCommandBase<CreatedLottoRegularAndDoubleImageResult>, ICreateLottoTicketImageCommand<CreatedLottoRegularAndDoubleImageResult>
    {
        private readonly ILogger<CreateLottoRegualrAndDoubleTicketImageCommand> _logger;

        public CreateLottoRegualrAndDoubleTicketImageCommand(ITicketRenderer ticketRenderer, ILogger<CreateLottoRegualrAndDoubleTicketImageCommand> logger)
            :base(ticketRenderer, logger)
        {
            _logger = logger;
        }
    }
}
