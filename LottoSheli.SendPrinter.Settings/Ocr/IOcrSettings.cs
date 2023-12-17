namespace LottoSheli.SendPrinter.Settings.OcrSettings
{
    public interface IOcrSettings
    {

        string OcrSettingsProfileName { get; }

        /// <summary>
        /// minimal width of rectangle
        /// </summary>
        int HorBlobsFillThreshold { get; }

        /// <summary>
        /// minimal height of rectangle
        /// </summary>
        int HorBlobsHeightThreshold { get; }

        /// <summary>
        /// specifies maximum horizontal gap between white pixels to fill.
        /// If number of black pixels between some white pixels is bigger than this
        /// value, then those black pixels are left as is; otherwise the gap is filled
        /// with white pixels.
        /// </summary>
        int MaxGapSize { get; }

        int UserIdBoxIndex { get; }

        int CropLineBoxIndex { get; }

        int? BottomCropLineBoxReverseIndex { get; }

        int UserIdContrastApplyLevel { get; }

        /// <summary>
        /// number of pixels to crop from left and right edges on a processed image
        /// </summary>
        int BitmapCropMargin { get; }

        /// <summary>
        /// scaling factor 
        /// used to scale preprocessed image on 'x' and 'y' axises where we should recognize text
        /// used for user if recognition
        /// </summary>
        int UserIdScaleFactor { get; }

        /// <summary>
        /// Width of part of ticket image with user id after rescaling x and y axises by TextScaleFactor
        /// used for user id recognition
        /// sometimes scanner stretches this part of image, 
        /// so values are set empiricly
        /// </summary>
        /// <remarks>for ScaleFactor = 3 UserIdBlobNormalWidthAfterScaling = 2495</remarks>
        int UserIdBlobNormalWidthAfterScaling { get; }

        /// <summary>
        /// Height of part of ticket image with user id after rescaling x and y axises by TextScaleFactor
        /// used for user id recognition
        /// sometimes scanner stretches this part of image, 
        /// so values are set empiricly
        /// </summary>
        /// <remarks>for ScaleFactor = 3 UserIdBlobNormalHeightAfterScaling = 135</remarks>
        int UserIdBlobNormalHeightAfterScaling { get; }

        /// <summary>
        /// used to trim user Id from parsed text from user Id section
        /// </summary>
        int UserIdLength { get; }

        /// <summary>
        /// if barcode space width is greater than this const then 
        /// the distance to the space is calculated as follows:
        /// spaces index + spaces width/2
        /// </summary>
        int BarcodeSpaceWidthThreshold { get; }

        /// <summary>
        /// minimal number of spaces to be present in valid barcode
        /// </summary>
        int BarcodeMinNumberOfSpaces { get; }

        /// <summary>
        /// number of spaces to scan from left and right sides of barcode
        /// left and right values are used to get approximate bounds of barcode
        /// </summary>
        int BarcodeNumOfSpacesToScan { get; }

        /// <summary>
        /// expected data length to be present in bottom barcode
        /// </summary>
        int BarcodeExpectedDataLength { get; }

        /// <summary>
        /// expected minimal data length to be present in top barcode
        /// </summary>
        int TopBarcodeMinDataLength { get; }

        /// <summary>
        ///used to ignore possible top dark areas (result of shift while scanning or other human error)
        /// </summary>
        int TopOffsetThreshold { get; }

        int BottomBarcodeHeight { get; }

        int BottomBarcodeYOffset { get; }

        int BottomBarcodeXMargin { get; }

        int UserIdPaddingX { get; }

        int UserIdPaddingY { get; }

        /// <summary>
        /// minimal 'extra' madigit size in pixels
        /// </summary>
        int MinExtraDigitSize { get; }

        /// <summary>
        /// when recognizing 'extra' mark its area is enlarged by 'x' and 'y' axises
        /// by this scale factor
        /// </summary>
        int ExtraScaleFactor { get; }

        /// <summary>
        /// 'etalon 'extra' mark proportion of sides
        /// used to find whether current 'extra' mark proportions are equal with this
        /// empiricly received one in range of some deviation
        /// </summary>
        float ExtraMarkerAspect { get; }

        /// <summary>
        /// acceptable shift angle between 'axtra' mark top vector and bottom vector
        /// </summary>
        float ExtraMarkerAngleTolerance { get; }

        /// <summary>
        /// minimal 'extra' mark width in pixels
        /// </summary>
        int ExtraBlobFilterMinSize { get; }

        /// <summary>
        /// User ID Maximal height
        /// </summary>
        int UserIdMaximalHeight { get; }

        /// <summary>
        /// User ID Minimal height
        /// </summary>
        int UserIdMinimallHeight { get; }

        /// <summary>
        /// User ID Maximal width
        /// </summary>
        int UserIdMaximalWidth { get; }

        /// <summary>
        /// User ID Minimal width
        /// </summary>
        int UserIdMinimallWidth { get; }

        /// <summary>
        /// Tesseract V3 data path
        /// </summary>
        string TessnetPath { get; }

        /// <summary>
        /// Tesseract V4 data path
        /// </summary>
        string TessnetV4Path { get; }

        /// <summary>
        /// Bottom crop line static offset
        /// </summary>
        int BottomCropLineOffset { get; }


        int ExtraMarkerHeightBottomLimit { get; }

        int ExtraMarkerWidthBottomLimit { get; }

        int ExtraMarkerWidthTopLimit { get; }

        int NationalIdThreshold { get; set; }

        int TicketThreshold { get; set; }

        string Description { get; }

        /// <summary>
        /// Path to folder containing templates for different ticket types
        /// </summary>
        string SlipTemplatePath { get; }

        /// <summary>
        /// Path to folder where unrecognized scans are stored
        /// </summary>
        string UnrecognizedScanPath { get; }
    }
}