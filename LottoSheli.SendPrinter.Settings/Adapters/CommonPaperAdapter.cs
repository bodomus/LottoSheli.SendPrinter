using LottoSheli.SendPrinter.Settings.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LottoSheli.SendPrinter.Settings.Adapters
{
    public class CommonPaperAdapter : BaseAdapter<PaperSettings>, ISettings
    {
        protected override string _fileName => throw new NotImplementedException();

        protected readonly bool _debugMode;

        public event EventHandler<string> StationIdChanged;

        public CommonPaperAdapter(bool debug) : base()
        {
            _debugMode = debug;
        }

        /// <summary>
        /// Server Url
        /// </summary>
        public string ServerUrl
        {
            get => _currentSettings.ServerUrl;
            set => _currentSettings.ServerUrl = value;
        }

        /// <summary>
        /// Drupal 7 Server Url
        /// </summary>
        public Uri D7ServerUrl
        {
            get
            {
                return Uri.IsWellFormedUriString(_currentSettings.ServerUrl, UriKind.Absolute) 
                    ? new Uri(Uri.EscapeUriString(_currentSettings.ServerUrl)) 
                    : throw new ArgumentException("No valid server Url found in settings");
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        [JsonIgnore]
        public string Login
        {
            get => _currentSettings.Login;
            set => _currentSettings.Login = value;
        }

        /// <summary>
        /// Passwrod
        /// </summary>
        [JsonIgnore]
        public string Password
        {
            get => _currentSettings.Password;
            set => _currentSettings.Password = value;
        }

        /// <summary>
        /// D9 Server Url
        /// </summary>
        public string D9ServerUrl
        {
            get => _currentSettings.D9ServerUrl;
            set => _currentSettings.D9ServerUrl = value;
        }

        /// <summary>
        /// D9 Access Key
        /// </summary>
        [JsonIgnore]
        public string D9AccessKey
        {
            get => _currentSettings.D9AccessKey;
            set => _currentSettings.D9AccessKey = value;
        }

        /// <summary>
        /// user session data. Used to check if a user can continue work or needs to login again
        /// </summary>
        [JsonIgnore]
        public string Session
        {
            get => _currentSettings.Session;
            set => _currentSettings.Session = value;
        }

        /// <summary>
        /// user D9 session data. Used to check if a user can continue work or needs to login again
        /// </summary>
        [JsonIgnore]
        public string D9Session
        {
            get => _currentSettings.D9Session;
            set => _currentSettings.D9Session = value;
        }

        /// <summary>
        /// Printer name
        /// </summary>
        public string PrinterName
        {
            get => _currentSettings.PrinterName;
            set => _currentSettings.PrinterName = value;
        }

        /// <summary>
        /// printing profiles for all game types and for credit printing
        /// </summary>
        /// <param name="name">profile name</param>
        /// <returns>printer settings for specific profile</returns>
        public PrintSettings GetPrintProfile(string name)
        {
            return _currentSettings.PrinterSettings[name];
        }

        /// <summary>
        /// All printer specific settings
        /// </summary>
        public IDictionary<string, PrintSettings> PrinterSettings
        {
            get => _currentSettings.PrinterSettings;
            set => _currentSettings.PrinterSettings = value;
        }

        /// <summary>
        /// \deprecated. Used to maintain info whether app is in debug mode
        /// </summary>
        public bool DebugMode => _debugMode;

        /// <summary>
        /// brightness of ticket header text
        /// </summary>
        public int TicketLabelBrightness
        {
            get => _currentSettings.TicketLabelBrightness;
            set => _currentSettings.TicketLabelBrightness = value;
        }

        /// <summary>
        /// font size of ticket header text
        /// </summary>
        public int TicketLabelFontSize
        {
            get => _currentSettings.TicketLabelFontSize;
            set => _currentSettings.TicketLabelFontSize = value;
        }

        /// <summary>
        /// ticket header text start position from left paper edge
        /// </summary>
        public int TicketLabelXOffset
        {
            get => _currentSettings.TicketLabelXOffset;
            set => _currentSettings.TicketLabelXOffset = value;
        }

        /// <summary>
        /// ticket header text start position from top paper edge
        /// </summary>
        public int TicketLabelYOffset
        {
            get => _currentSettings.TicketLabelYOffset;
            set => _currentSettings.TicketLabelYOffset = value;
        }

        /// <summary>
        /// \deprecated.  
        /// </summary>
        public bool TicketLabelBottomLineRelative
        {
            get => _currentSettings.TicketLabelBottomLineRelative;
            set => _currentSettings.TicketLabelBottomLineRelative = value;
        }

        /// <summary>
        /// Station Id. Usually means exact printer in a network on which tickets are going to be scanned
        /// </summary>
        public string StationId
        {
            get => _currentSettings.StationID;
            set
            {
                _currentSettings.StationID = value;
                StationIdChanged?.Invoke(null, value);
            }
        }

        /// <summary>
        /// Printed Station Id. The station ID which should be printed on the ticket
        /// </summary>
        public string PrintedStationId
        {
            get => _currentSettings.PrintedStationID;
            set => _currentSettings.PrintedStationID = value;
        }

        public bool PrintTicketFrame
        {
            get => _currentSettings.PrintTicketFrame;
            set => _currentSettings.PrintTicketFrame = value;
        }

        public int MaxRequestConnectionsLimit
        {
            get => _currentSettings.MaxRequestConnectionsLimit;
            set => _currentSettings.MaxRequestConnectionsLimit = value;
        }

        public int MaxInterval
        {
            get => _currentSettings.MaxInterval;
            set => _currentSettings.MaxInterval = value;
        }

        public int MinRegularChanceType_3_WinAmountToCheckUserId
        {
            get => _currentSettings.MinRegularChanceType_3_WinAmountToCheckUserId;
            set => _currentSettings.MinRegularChanceType_3_WinAmountToCheckUserId = value;
        }

        public int MinRegularChanceType_4_WinAmountToCheckUserId
        {
            get => _currentSettings.MinRegularChanceType_4_WinAmountToCheckUserId;
            set => _currentSettings.MinRegularChanceType_4_WinAmountToCheckUserId = value;
        }

        public int MinRegularChanceType_2_WinAmountToCheckUserId
        {
            get => _currentSettings.MinRegularChanceType_2_WinAmountToCheckUserId;
            set => _currentSettings.MinRegularChanceType_2_WinAmountToCheckUserId = value;
        }

        public int MinRegularChanceType_5_WinAmountToCheckUserId
        {
            get => _currentSettings.MinRegularChanceType_5_WinAmountToCheckUserId;
            set => _currentSettings.MinRegularChanceType_5_WinAmountToCheckUserId = value;
        }

        public int MaxRegularChanceType_3_WinAmountToCheckUserId
        {
            get => _currentSettings.MaxRegularChanceType_3_WinAmountToCheckUserId;
            set => _currentSettings.MaxRegularChanceType_3_WinAmountToCheckUserId = value;
        }

        public int MaxRegularChanceType_4_WinAmountToCheckUserId
        {
            get => _currentSettings.MaxRegularChanceType_4_WinAmountToCheckUserId;
            set => _currentSettings.MinRegularChanceType_4_WinAmountToCheckUserId = value;
        }

        public int MaxRegularChanceType_2_WinAmountToCheckUserId
        {
            get => _currentSettings.MaxRegularChanceType_2_WinAmountToCheckUserId;
            set => _currentSettings.MaxRegularChanceType_2_WinAmountToCheckUserId = value;
        }

        public int MaxRegularChanceType_5_WinAmountToCheckUserId
        {
            get => _currentSettings.MaxRegularChanceType_5_WinAmountToCheckUserId;
            set => _currentSettings.MaxRegularChanceType_5_WinAmountToCheckUserId = value;
        }

        public int Min123WinAmountToCheckUserId
        {
            get => _currentSettings.Min123WinAmountToCheckUserId;
            set => _currentSettings.Min123WinAmountToCheckUserId = value;
        }

        public int Min123TableAmountToCheckUserId
        {
            get => _currentSettings.Min123TableAmountToCheckUserId;
            set => _currentSettings.Min123TableAmountToCheckUserId = value;
        }

        public int MaxRetries
        {
            get => _currentSettings.MaxRetries;
            set => _currentSettings.MaxRetries = value;
        }

        public int Scanner_SnippetRectangle_X
        {
            get => _currentSettings.Scanner_SnippetRectangle_X;
            set => _currentSettings.Scanner_SnippetRectangle_X = value;
        }
        public int Scanner_SnippetRectangle_Y
        {
            get => _currentSettings.Scanner_SnippetRectangle_Y;
            set => _currentSettings.Scanner_SnippetRectangle_Y = value;
        }
        public int Scanner_SnippetRectangle_Width
        {
            get => _currentSettings.Scanner_SnippetRectangle_Width;
            set => _currentSettings.Scanner_SnippetRectangle_Width = value;
        }
        public int Scanner_SnippetRectangle_Height
        {
            get => _currentSettings.Scanner_SnippetRectangle_Height;
            set => _currentSettings.Scanner_SnippetRectangle_Height = value;
        }
        public int Scanner_SnippetSide
        {
            get => _currentSettings.Scanner_SnippetSide;
            set => _currentSettings.Scanner_SnippetSide = value;
        }
        public int Scanner_ImageAdjusments_Contrast
        {
            get => _currentSettings.Scanner_ImageAdjusments_Contrast;
            set => _currentSettings.Scanner_ImageAdjusments_Contrast = value;
        }
        public int Scanner_ImageAdjusments_Brightness
        {
            get => _currentSettings.Scanner_ImageAdjusments_Brightness;
            set => _currentSettings.Scanner_ImageAdjusments_Brightness = value;
        }

        public bool AutoupdateWinnersTemplate 
        {
            get => _currentSettings.AutoupdateWinnersTemplate;
            set => _currentSettings.AutoupdateWinnersTemplate = value;
        }

        public bool CollectTrainingData 
        {
            get => _currentSettings.CollectTrainingData;
            set => _currentSettings.CollectTrainingData = value;
        }

        public WinnerSettings WinnerSettings
        {
            get => _currentSettings.WinnerSettings;
            set => _currentSettings.WinnerSettings = value;
        }
    }
}
