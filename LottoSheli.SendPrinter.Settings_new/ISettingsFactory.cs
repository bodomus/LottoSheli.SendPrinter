using LottoSheli.SendPrinter.Settings.Factory.OcrSettings;
using LottoSheli.SendPrinter.Settings.Factory.RemoteSettings;

namespace LottoSheli.SendPrinter.Settings
{
    public interface ISettingsFactory
    {
        Factory.ScannerSettings.ScannerSettings GetScannerSettings();

        void SaveScannerSettings(Factory.ScannerSettings.ScannerSettings settings);


        IRemoteSettings GetRemoteSettings();

        IOcrSettings GetOcrSettings();
    }
}
