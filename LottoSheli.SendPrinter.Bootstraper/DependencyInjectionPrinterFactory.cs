using LottoSheli.SendPrinter.Printer;
using LottoSheli.SendPrinter.Printer.Devices;
using LottoSheli.SendPrinter.Printer.Renderers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LottoSheli.SendPrinter.Bootstraper
{
    class DependencyInjectionPrinterFactory : IPrinterFactory
    {
        private Func<IServiceProvider> _serviceProviderStrategy;
        public DependencyInjectionPrinterFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public IPrinterDevice GetPrinterDevice()
        {
            return _serviceProviderStrategy().GetRequiredService<IPrinterDevice>();
        }

        public ITicketRenderer GetTicketRenderer()
        {
            return _serviceProviderStrategy().GetRequiredService<ITicketRenderer>();
        }
    }
}
