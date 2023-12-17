using Microsoft.Extensions.DependencyInjection;
using System;
using LottoSheli.SendPrinter.Settings.RemoteSettings;
using LottoSheli.SendPrinter.Settings.ScannerSettings;
using LottoSheli.SendPrinter.Settings.OcrSettings;

namespace LottoSheli.SendPrinter.Settings
{
    class DependencyInjectionSettingsFactory : ISettingsFactory
    {
        private Func<IServiceProvider> _serviceProviderStrategy;
        public DependencyInjectionSettingsFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public ScannerSettings.ScannerSettings GetScannerSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<ScannerSettingsService>().Get();
        }

        public void SaveScannerSettings(ScannerSettings.ScannerSettings settings)
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
