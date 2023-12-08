using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates regular and double lotto ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<CreatedChanceTicketImageResult>))]
    public class CreateChanceTicketImageCommand : CreateChanceTicketImageBase<CreatedChanceTicketImageResult>, ICreateLottoTicketImageCommand<CreatedChanceTicketImageResult>
    {
        public CreateChanceTicketImageCommand(ITicketRenderer ticketRenderer, ILogger<CreateChanceTicketImageCommand> logger)
            :base(ticketRenderer, logger)
        {
        }

        protected override string LayoutName => "Chance";

        protected override bool SpecifyChanceType => true;
    }
}
