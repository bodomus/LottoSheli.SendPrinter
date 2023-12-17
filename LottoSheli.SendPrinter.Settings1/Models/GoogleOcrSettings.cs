using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Settings.Models
{
    public class GoogleOcrSettings
    {
        public bool Enabled { get; set; }
        public bool RecognizeLottoEnabled { get; set; }
        public bool RecognizeChanceEnabled { get; set; }
        public bool Recognize777Enabled { get; set; }
        public bool Recognize123Enabled { get; set; }

        public int TicketThreshold { get; set; }
        public bool TicketThresholdEnabled { get; set; }

        public bool Inverted { get; set; }

        public string CredentialsJSON {get;set;}
    }
}
