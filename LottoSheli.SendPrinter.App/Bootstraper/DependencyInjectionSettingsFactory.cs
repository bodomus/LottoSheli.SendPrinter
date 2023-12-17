using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.Settings.OcrSettings;
using LottoSheli.SendPrinter.Settings.RemoteSettings;
using LottoSheli.SendPrinter.Settings.ScannerSettings;
using Microsoft.Extensions.DependencyInjection;

namespace LottoSheli.SendPrinter.Bootstraper
{
    class DependencyInjectionSettingsFactory : ISettingsFactory
    {
        private Func<IServiceProvider> _serviceProviderStrategy;
        public DependencyInjectionSettingsFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public ScannerSettings GetScannerSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<ScannerSettingsService>().Get();
        }

        public void SaveScannerSettings(ScannerSettings settings)
        {
            _serviceProviderStrategy().GetRequiredService<ScannerSettingsService>().Save(settings);
        }

        public IOcrSettings GetOcrSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<IOcrSettings>();
        }

        public IRemoteSettings GetRemoteSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<IRemoteSettings>();
        }

        
    }
}