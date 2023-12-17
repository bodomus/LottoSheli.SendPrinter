using LottoSheli.SendPrinter.Settings.OcrSettings;
using LottoSheli.SendPrinter.Settings.RemoteSettings;
using LottoSheli.SendPrinter.Settings.ScannerSettings;

namespace LottoSheli.SendPrinter.Settings
{
    public interface ISettingsFactory
    {
        ScannerSettings.ScannerSettings GetScannerSettings();

        void SaveScannerSettings(ScannerSettings.ScannerSettings settings);


        IRemoteSettings GetRemoteSettings();

        IOcrSettings GetOcrSettings();
    }
}
