using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.Print.Base
{
    public abstract class CreatedImageResultBase
    {
        public abstract Bitmap Image { get; init; }
    }
}
