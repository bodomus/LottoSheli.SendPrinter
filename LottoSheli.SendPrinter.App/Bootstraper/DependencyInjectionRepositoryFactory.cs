using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LottoSheli.SendPrinter.Bootstraper
{
    class DependencyInjectionRepositoryFactory : IRepositoryFactory
    {
        private Func<IServiceProvider> _serviceProviderStrategy;
        public DependencyInjectionRepositoryFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public TRepo GetRepository<TRepo>() where TRepo : IBaseRepository
        {
            return _serviceProviderStrategy().GetRequiredService<TRepo>();
        }
    }
}
