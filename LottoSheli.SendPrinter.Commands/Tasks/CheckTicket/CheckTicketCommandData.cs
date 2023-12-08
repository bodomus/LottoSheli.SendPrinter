using LottoSheli.SendPrinter.Commands.Base;
using System.Drawing;
using Tesseract;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    /// <summary>
    /// The data for <see cref="IRecognizeOrderedTicketCommand"/>
    /// </summary>
    public class CheckTicketCommandData : ICommandData
    {
        /// <summary>
        /// Ticket image
        /// </summary>
        public Bitmap Ticket { get; set; }
        
        /// <summary>
        /// Image filter threshold
        /// </summary>
        public int ImageThreshold { get; set; }

        /// <summary>
        /// specifies maximum horizontal gap between white pixels to fill.
        /// If number of black pixels between some white pixels is bigger than this
        /// value, then those black pixels are left as is; otherwise the gap is filled
        /// with white pixels.
        /// </summary>
        public int MaxGapSize { get; set; }

        /// <summary>
        /// minimal width of rectangle
        /// </summary>
        public int HorBlobsHeightThreshold { get; set; }

        /// <summary>
        /// minimal height of rectangle
        /// </summary>
        public int HorBlobsFillThreshold { get; set; }

        /// <summary>
        /// Recognize SlipId Preprocess Strategy Name
        /// </summary>
        public string RecognizeSlipIdPreprocessStrategyName { get; set; }

        public int BottomBarcodeHeight { get; init; }

        public int BottomBarcodeYOffset { get; init; }

        public int BottomBarcodeXMargin { get; init; }
    }
}
