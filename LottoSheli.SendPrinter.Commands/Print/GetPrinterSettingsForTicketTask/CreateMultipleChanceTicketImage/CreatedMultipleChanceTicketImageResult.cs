using LottoSheli.SendPrinter.Commands.Print.Base;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class CreatedMultipleChanceTicketImageResult : CreatedImageResultBase
    {
        public override Bitmap Image { get; init; }
    }
}
