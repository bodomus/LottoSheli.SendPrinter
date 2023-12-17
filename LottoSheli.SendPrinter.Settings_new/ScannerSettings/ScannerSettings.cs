namespace LottoSheli.SendPrinter.Settings.ScannerSettings
{
    public class ScannerSettings : IScannerSettings
    {
        public int Scanner_SnippetRectangle_X { get; set; }
        public int Scanner_SnippetRectangle_Y { get; set; }
        public int Scanner_SnippetRectangle_Width { get; set; }
        public int Scanner_SnippetRectangle_Height { get; set; }
        public int Scanner_SnippetSide { get; set; }
        public int Scanner_ImageAdjusments_Contrast { get; set; }
        public int Scanner_ImageAdjusments_Brightness { get; set; }
        public bool AutoupdateWinnersTemplate { get; set; }
        public bool CollectTrainingData { get; set; }
        public int MaxTemplateVariants { get; set; }
    }
}
