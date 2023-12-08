using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates regular and double lotto ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<CreatedMultipleChanceTicketImageResult>))]
    public class CreateMultipleChanceTicketImageCommand : CreateChanceTicketImageBase<CreatedMultipleChanceTicketImageResult>, ICreateLottoTicketImageCommand<CreatedMultipleChanceTicketImageResult>
    {
        private readonly ILogger<CreateMultipleChanceTicketImageCommand> _logger;

        public CreateMultipleChanceTicketImageCommand(ITicketRenderer ticketRenderer, ILogger<CreateMultipleChanceTicketImageCommand> logger)
            :base(ticketRenderer, logger)
        {
            _logger = logger;
        }

        protected override string LayoutName => "MultipleChance";

        protected override bool SpecifyChanceType => false;
    }
}
