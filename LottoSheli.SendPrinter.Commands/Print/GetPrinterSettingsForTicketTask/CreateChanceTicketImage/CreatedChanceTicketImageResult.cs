using LottoSheli.SendPrinter.Commands.Print.Base;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class CreatedChanceTicketImageResult : CreatedImageResultBase
    {
        public override Bitmap Image { get; init; }
    }
}
