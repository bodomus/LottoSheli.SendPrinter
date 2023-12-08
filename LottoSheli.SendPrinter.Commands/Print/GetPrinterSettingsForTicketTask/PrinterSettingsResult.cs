using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class PrinterSettingsResult
    {
        /// <summary>
        /// printer settings profile name
        /// </summary>
        public string PrinterSettingsProfile { get; init; }

        /// <summary>
        /// description for printing
        /// </summary>
        public string DescriptionForPrint { get; init; }

        /// <summary>
        /// Bitmap creation strategy
        /// </summary>
        public Bitmap PrintedImage { get; init; }
    }
}
