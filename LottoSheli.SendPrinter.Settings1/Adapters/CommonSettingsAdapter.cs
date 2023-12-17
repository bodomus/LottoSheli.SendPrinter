using LottoSheli.SendPrinter.Settings.Models;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Settings.Adapters
{
    /// <summary>
    /// provides functionality to save and get common application settings
    /// </summary>
    public class CommonSettingsAdapter : BaseAdapter<CommonSettings>, ICommonSettings
    {
        protected override string _fileName => "CommonSettings";

        /// <summary>
        /// Returns if simple paper or Miphal HaPais mode settings are used 
        /// </summary>
        public bool UseSimplePaperSettings
        {
            get
            {
                return _currentSettings.UseSimplePaperSettings;
            }
            set
            {
                _currentSettings.UseSimplePaperSettings = value;
            }
        }
        /// <summary>
        /// Returns if common settings need to be refreshed or not
        /// </summary>
        public bool UpdateRequired
        {
            get
            {
                return _currentSettings.UpdateRequired;
            }
            set
            {
                _currentSettings.UpdateRequired = value;
            }
        }
        /// <summary>
        /// Returns user ID black list
        /// </summary>
        public List<string> BlackList
        {
            get
            {
                return _currentSettings.BlackList;
            }
            set
            {
                _currentSettings.BlackList = value;
            }
        }

        /// <summary>
        /// Returns user ID 4s compare
        /// </summary>
        public List<string> UserIDs
        {
            get
            {
                return _currentSettings.UserIDs;
            }
            set
            {
                _currentSettings.UserIDs = value;
            }
        }

        /// <summary>
        /// Returns default printer Id received from server after connection (done on program start)
        /// </summary>
        public int DefaultPrinterId
        {
            get
            {
                return _currentSettings.DefaultPrinterId;
            }
            set
            {
                _currentSettings.DefaultPrinterId = value;
            }
        }
        /// <summary>
        /// returns if new or old credit mode is used
        /// </summary>
        public bool UseNewCreditMode
        {
            get
            {
                return _currentSettings.UseNewCreditMode;
            }
            set
            {
                _currentSettings.UseNewCreditMode = value;
            }
        }

        /// <summary>
        /// URL of Zabbix server
        /// </summary>
        public string ZabbixUrl
        {
            get
            {
                return _currentSettings.ZabbixUrl;
            }
            set
            {
                _currentSettings.ZabbixUrl = value;
            }
        }
    }
}
