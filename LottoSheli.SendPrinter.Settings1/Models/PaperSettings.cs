using Newtonsoft.Json;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Settings.Models
{
    /// <summary>
    /// Paper setting common for Simple paper and Miphal Hapais
    /// </summary>
    public class PaperSettings
    {
        [JsonIgnore]
        public string Session { get; set; }

        [JsonIgnore]
        public string D9Session { get; set; }

        public string PrinterName { get; set; }

        public bool UpdateRequired { get; set; }

        public int TicketLabelBrightness { get; set; }

        public int TicketLabelXOffset { get; set; }

        public int TicketLabelYOffset { get; set; }

        public int TicketLabelFontSize { get; set; }

        public bool TicketLabelBottomLineRelative { get; set; }

        public bool SimpleMode { get; set; }

        public string StationID { get; set; }

        public string PrintedStationID { get; set; }

        public bool PrintTicketFrame { get; set; }

        public IDictionary<string, PrintSettings> PrinterSettings { get; set; }

        public int MaxRequestConnectionsLimit { get; set; }

        public int MaxInterval { get; set; }

        public int MaxRetries { get; set; }

        public int MinRegularChanceType_2_WinAmountToCheckUserId { get; set; }

        public int MinRegularChanceType_3_WinAmountToCheckUserId { get; set; }

        public int MinRegularChanceType_4_WinAmountToCheckUserId { get; set; }

        public int MinRegularChanceType_5_WinAmountToCheckUserId { get; set; }

        public int MaxRegularChanceType_2_WinAmountToCheckUserId { get; set; }

        public int MaxRegularChanceType_3_WinAmountToCheckUserId { get; set; }

        public int MaxRegularChanceType_4_WinAmountToCheckUserId { get; set; }

        public int MaxRegularChanceType_5_WinAmountToCheckUserId { get; set; }

        public int Min123WinAmountToCheckUserId { get; set; }

        public int Min123TableAmountToCheckUserId { get; set; }

        [JsonIgnore]
        public string Login { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string ServerUrl { get; set; }

        [JsonIgnore]
        public string D9AccessKey { get; set; }

        public string D9ServerUrl { get; set; }

        public int Scanner_SnippetRectangle_X { get; set; }
        public int Scanner_SnippetRectangle_Y { get; set; }
        public int Scanner_SnippetRectangle_Width { get; set; }
        public int Scanner_SnippetRectangle_Height { get; set; }
        public int Scanner_ImageAdjusments_Contrast { get; set; }
        public int Scanner_ImageAdjusments_Brightness { get; set; }
        public int Scanner_SnippetSide { get; set; }

        public bool AutoupdateWinnersTemplate { get; set; }

        public bool CollectTrainingData { get; set; }

        public WinnerSettings WinnerSettings { get; set; }
        
    }
}
