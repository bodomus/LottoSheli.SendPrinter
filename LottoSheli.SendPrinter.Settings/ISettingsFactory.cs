using LottoSheli.SendPrinter.Settings.Models;

namespace LottoSheli.SendPrinter.Settings
{
    public interface ISettingsFactory
    {
        ISettings GetSettings();

        ICommonSettings GetCommonSettings();

        IOcrSettings GetOcrSettings();
    }
}
