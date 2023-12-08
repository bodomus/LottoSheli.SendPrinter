using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Print.Base;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Commands.Print
{

    /// <summary>
    /// Creates regular and double lotto ticket print image by specified <see cref="CreateLottoTicketImageCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICreateLottoTicketImageCommand<CreatedMethodicalChanceTicketImageResult>))]
    public class CreateMethodicalChanceTicketImageCommand : CreateChanceTicketImageBase<CreatedMethodicalChanceTicketImageResult>, ICreateLottoTicketImageCommand<CreatedMethodicalChanceTicketImageResult>
    {
        public CreateMethodicalChanceTicketImageCommand(ITicketRenderer ticketRenderer, ILogger<CreateMethodicalChanceTicketImageCommand> logger)
            :base(ticketRenderer, logger)
        {
        }

        protected override string LayoutName => "MethodicalChance";

        protected override bool SpecifyChanceType => true;
    }
}
