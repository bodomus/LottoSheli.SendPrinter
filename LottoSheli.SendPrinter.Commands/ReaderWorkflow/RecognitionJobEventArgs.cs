using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow
{
    public class RecognitionJobEventArgs : EventArgs
    {
        public RecognitionJob Job { get; set; }
        public RecognitionJobStatus Status { get; set; }

        public RecognitionJobEventArgs() : base() { }
    }
}
