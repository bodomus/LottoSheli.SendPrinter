namespace LottoSheli.SendPrinter.Settings.Models
{
    public class OcrSettings
    {
        public int HorBlobsFillThreshold { get; set; }

        public int HorBlobsHeightThreshold { get; set; }

        public int MaxGapSize { get; set; }

        public int BitmapCropMargin { get; set; }

        public int UserIdScaleFactor { get; set; }

        public int UserIdBlobNormalWidth { get; set; }

        public int UserIdBlobNormalHeight { get; set; }

        public int UserIdLength { get; set; }

        public int BarcodeSpaceWidthThreshold { get; set; }

        public int BarcodeMinNumberOfSpaces { get; set; }

        public int BarcodeNumOfSpacesToScan { get; set; }

        public int BarcodeExpectedDataLength { get; set; }

        public int TopBarcodeMinDataLength { get; set; }

        public int TopOffsetThreshold { get; set; }

        public int BottomBarcodeHeight { get; set; }

        public int BottomBarcodeYOffset { get; set; }

        public int BottomBarcodeXMargin { get; set; }

        public int MinExtraDigitSize { get; set; }

        public int ExtraScaleFactor { get; set; }

        public float ExtraMarkerAspect { get; set; }

        public float ExtraMarkerAngleTolerance { get; set; }

        public int ExtraBlobFilterMinSize { get; set; }

        public int UserIdBoxIndex { get; set; }

        public int CropLineBoxIndex { get; set; }

        public int? BottomCropLineBoxReverseIndex { get; set; }

        public int UserIdContrastApplyLevel { get; set; }

        public int UserIdPaddingX { get; set; }

        public int UserIdPaddingY { get; set; }

        /// <summary>
        /// User ID Maximal height
        /// </summary>
        public int UserIdMaximalHeight { get; set; }

        /// <summary>
        /// User ID Minimal height
        /// </summary>
        public int UserIdMinimallHeight { get; set; }

        /// <summary>
        /// User ID Maximal width
        /// </summary>
        public int UserIdMaximalWidth { get; set; }

        /// <summary>
        /// User ID Minimal width
        /// </summary>
        public int UserIdMinimallWidth { get; set; }

        /// <summary>
        /// Tesseract V3 data path
        /// </summary>
        public string TessnetPath { get; set; }

        /// <summary>
        /// Tesseract V4 data path
        /// </summary>
        public string TessnetV4Path { get; set; }

        public int ExtraMarkerHeightBottomLimit { get; set; }

        public int ExtraMarkerWidthBottomLimit { get; set; }

        public int ExtraMarkerWidthTopLimit { get; set; }

        public int TicketThreshold { get; set; }

        public int NationalIdThreshold { get; set; }

        public GoogleOcrSettings GoogleOcrSettings { get; set; }

        public RecognitionStrategiesSettings RecognitionStrategies { get; set; }

        public string Description { get; set; }

        public int BottomCropLineOffset { get; set; }

        public string SlipTemplatePath { get; set; }

        public string UnrecognizedScanPath { get; set; }
    }
}
