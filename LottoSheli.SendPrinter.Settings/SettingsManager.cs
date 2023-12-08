using System;
using System.IO;
using LottoSheli.SendPrinter.Settings.Adapters;
using LottoSheli.SendPrinter.Settings.Models;

namespace LottoSheli.SendPrinter.Settings
{
    /// <summary>
    /// Used to manage common application settings and return current paper settings
    /// </summary>
    public static class SettingsManager
    {
        private static string ocrSettingsVersion = "V2";
        private static CommonSettingsAdapter commonSettings = new CommonSettingsAdapter();

        private static OcrSettingsAdapter ocrSettings = new OcrSettingsAdapter();

        

        private static SettingsSimplePaperAdapter settingsSimplePaperSettings = new SettingsSimplePaperAdapter();

        private static SettingsMiphalHapaisPaperAdapter settingsMiphalHapaisPaperSettings = new SettingsMiphalHapaisPaperAdapter();
        public static string LottoHome => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "LottoSendPrinter");

        public static string OcrSettingsVersion { get => ocrSettingsVersion; set => ocrSettingsVersion = value; }

        public static IOcrSettings GetOcrSettings()
        {
            return ocrSettings;
        }

        public static void SaveOcrSettings()
        {
            ocrSettings.SaveOcrSettings();
        }

        /// <summary>
        /// Common 
        /// </summary>
        /// <returns></returns>
        public static ICommonSettings GetCommonSettings()
        {
            return commonSettings;
        }
        /// <summary>
        /// Used as a common entry point to get settings which implements ISettings interface
        /// </summary>
        /// <param name="debug">deprecated - used to preserve state whether app is launched in debugging mode or not</param>
        /// <returns>instance of settings</returns>
        public static ISettings GetSettings(bool debug = false)
        {
            if (commonSettings.UseSimplePaperSettings)
            {
                return settingsSimplePaperSettings;
            }
            else
            {
                return settingsMiphalHapaisPaperSettings;
            }
        }

        /// <summary>
        /// Switches between specific settings which implements ISettings interface
        /// </summary>
        /// <param name="useSimple">Except common Settings we use only 2 types of Settings (simple paper and Miphal Hapais Paper), 
        /// so simple bool flag is enough. With addition of new types of Settings it might be changed to enumeration of flags</param>
        public static ISettings SwitchSettings(bool useSimple)
        {
            commonSettings.UseSimplePaperSettings = useSimple;
            commonSettings.Save();
            return GetSettings();
        }

        /// <summary>
        /// Adds full user Id to Black List stored in Global Settings
        /// </summary>
        /// <param name="id">full user Id </param>
        public static void AddToBlackList(string id)
        {
            commonSettings.BlackList.Add(id);
            commonSettings.Save();
        }

        /// <summary>
        /// Removes from Black List full user Id 
        /// </summary>
        /// <param name="id">full user Id </param>
        public static void RemoveFromBlackList(string id)
        {
            commonSettings.BlackList.Remove(id);
            commonSettings.Save();
        }

        /// <summary>
        /// Checks whether user is in black list or not
        /// </summary>
        /// <param name="id">full user Id </param>
        /// <returns>Returns whether user is in black list or not</returns>
        public static bool CheckBlackList(string id)
        {
            return commonSettings.BlackList.Contains(id);            
        }

        /// <summary>
        /// Switches betweeen credit modes
        /// </summary>
        /// <param name="useNew">Currently we have 2 modes (new and old), so simple bool flag is enough</param>
        public static void SwitchCreditMode(bool useNew)
        {
            commonSettings.UseNewCreditMode = useNew;
            commonSettings.Save();
        }

        /// <summary>
        /// Sets default printer Id
        /// </summary>
        /// <param name="id">printer Id</param>
        public static void DefaultPrinterId(int id)
        {
            commonSettings.DefaultPrinterId = id;
            commonSettings.Save();
        }
    }
}
