using LottoSheli.SendPrinter.App.ui.login;
using LottoSheli.SendPrinter.App.View;
using LottoSheli.SendPrinter.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.App.ui
{
    public class FormFactory : IFormFactory
    {

        private Func<IServiceProvider> _serviceProviderStrategy;
        public FormFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public IOcrSettings GetOcrSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<IOcrSettings>();
        }

        public ILoginView CreateLoginForm()
        {
            return _serviceProviderStrategy().GetRequiredService<ILoginView>();
        }
    }
}
