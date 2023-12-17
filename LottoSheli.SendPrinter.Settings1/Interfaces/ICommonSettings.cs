using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Settings
{
    /// <summary>
    /// interface for any application common settings instance
    /// descriptions of each function and property see at specific implementations
    /// </summary>
    public interface ICommonSettings
    {
        bool UseSimplePaperSettings { get; }
        bool UpdateRequired { get; }
        List<string> BlackList { get; }
        int DefaultPrinterId { get; }
        bool UseNewCreditMode { get; }
        List<string> UserIDs { get; }
        string ZabbixUrl { get; set; }
        void Save();
    }
}
