using LottoSheli.SendPrinter.Remote;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LottoSheli.SendPrinter.Bootstraper
{
    class DependencyInjectionRemoteFactory : IRemoteFactory
    {
        private Func<IServiceProvider> _serviceProviderStrategy;
        public DependencyInjectionRemoteFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public IRemoteClient GetRemoteClient() =>
            _serviceProviderStrategy().GetRequiredService<IRemoteClient>();

        public ISessionManagerFactory GetSessionManagerFactory() => 
            _serviceProviderStrategy().GetRequiredService<ISessionManagerFactory>();

        public ISendingQueue GetSendingQueue() =>
            _serviceProviderStrategy().GetRequiredService<ISendingQueue>();
    }
}
