using Newtonsoft.Json;


namespace LottoSheli.SendPrinter.Settings
{
    /// <summary>
    /// interface for any application paper settings instance
    /// descriptions of each function and property see at specific implementations
    /// </summary>
    public interface ISettings
    {
        string ServerUrl { get; set; }

        [JsonIgnore]
        Uri D7ServerUrl { get; }

        [JsonIgnore]
        string Login { get; set; }

        [JsonIgnore]
        string Password { get; set; }

        [JsonIgnore]
        string Session { get; set; }

        string PrinterName { get; set; }

        int MaxRequestConnectionsLimit { get; set; }
        int MaxInterval { get; set; }
        int MaxRetries { get; set; }

        int MinRegularChanceType_2_WinAmountToCheckUserId { get; set; }

        int MinRegularChanceType_3_WinAmountToCheckUserId { get; set; }

        int MinRegularChanceType_4_WinAmountToCheckUserId { get; set; }

        int MinRegularChanceType_5_WinAmountToCheckUserId { get; set; }

        int MaxRegularChanceType_2_WinAmountToCheckUserId { get; set; }

        int MaxRegularChanceType_3_WinAmountToCheckUserId { get; set; }

        int MaxRegularChanceType_4_WinAmountToCheckUserId { get; set; }

        int MaxRegularChanceType_5_WinAmountToCheckUserId { get; set; }

        int Min123WinAmountToCheckUserId { get; set; }

        int Min123TableAmountToCheckUserId { get; set; }

        bool DebugMode { get; }

        event EventHandler<string> StationIdChanged;

        int TicketLabelBrightness { get; set; }
        int TicketLabelFontSize { get; set; }
        int TicketLabelXOffset { get; set; }
        int TicketLabelYOffset { get; set; }
        bool TicketLabelBottomLineRelative { get; set; }

        string StationId { get; set; }

        string PrintedStationId { get; set; }

        bool PrintTicketFrame { get; set; }

        void Save();
        string D9ServerUrl { get; set; }

        [JsonIgnore]
        string D9AccessKey { get; set; }

        int Scanner_SnippetRectangle_X { get; set; }
        int Scanner_SnippetRectangle_Y { get; set; }
        int Scanner_SnippetRectangle_Width { get; set; }
        int Scanner_SnippetRectangle_Height { get; set; }
        int Scanner_SnippetSide { get; set; }
        int Scanner_ImageAdjusments_Contrast { get; set; }
        int Scanner_ImageAdjusments_Brightness { get; set; }
        bool AutoupdateWinnersTemplate { get; set; }
        bool CollectTrainingData { get; set; }
    }
}
