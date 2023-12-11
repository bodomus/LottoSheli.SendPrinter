using LottoSheli.SendPrinter.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LottoSheli.SendPrinter.Bootstraper
{
    class DependencyInjectionSettingsFactory : ISettingsFactory
    {
        private Func<IServiceProvider> _serviceProviderStrategy;
        public DependencyInjectionSettingsFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public IOcrSettings GetOcrSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<IOcrSettings>();
        }

        public ISettings GetSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<ISettings>();
        }

        public ICommonSettings GetCommonSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<ICommonSettings>();
        }
    }
}
