using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Settings.Models
{
    public class CommonSettings
    {
        public bool UpdateRequired { get; set; }
        public List<string> BlackList { get; set; }
        public bool UseNewCreditMode { get; set; }
        public int DefaultPrinterId { get; set; }
        public bool UseSimplePaperSettings { get; set; }
        public List<string> UserIDs { get; set; }
        public string ZabbixUrl { get; set; }
    }
}
