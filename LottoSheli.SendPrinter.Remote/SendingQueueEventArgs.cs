using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    public class SendingQueueEventArgs : EventArgs
    {
        public RecognitionJob Job { get; set; }
        public int Sending { get; set; } = 0;
        public int Pending { get; set; } = 0;
        public int Sent { get; set; } = 0;
    }
}
