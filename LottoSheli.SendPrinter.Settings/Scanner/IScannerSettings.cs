namespace LottoSheli.SendPrinter.Settings.ScannerSettings
{
    public interface IScannerSettings
    {
        int Scanner_SnippetRectangle_X { get; set; }
        int Scanner_SnippetRectangle_Y { get; set; }
        int Scanner_SnippetRectangle_Width { get; set; }
        int Scanner_SnippetRectangle_Height { get; set; }
        int Scanner_SnippetSide { get; set; }
        int Scanner_ImageAdjusments_Contrast { get; set; }
        int Scanner_ImageAdjusments_Brightness { get; set; }
        bool AutoupdateWinnersTemplate { get; set; }
        bool CollectTrainingData { get; set; }
        public int MaxTemplateVariants { get; set; }
    }
}