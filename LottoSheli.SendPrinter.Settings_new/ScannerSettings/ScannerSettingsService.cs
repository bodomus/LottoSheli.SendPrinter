using Microsoft.Extensions.Configuration;

namespace LottoSheli.SendPrinter.Settings.ScannerSettings
{
    public class ScannerSettingsService : Service<Factory.ScannerSettings.ScannerSettings>
    {
        public ScannerSettingsService(IConfiguration config) : base(config)
        {

        }

        protected override string SectionName => "ScannerSettings";

    }
}
