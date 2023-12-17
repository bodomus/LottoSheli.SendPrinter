using LottoSheli.SendPrinter.App.ui.login;
using LottoSheli.SendPrinter.App.View;
using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.Settings.OcrSettings;
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

        public ILoginView CreateLoginForm()
        {
            return _serviceProviderStrategy().GetRequiredService<ILoginView>();
        }
    }
}
