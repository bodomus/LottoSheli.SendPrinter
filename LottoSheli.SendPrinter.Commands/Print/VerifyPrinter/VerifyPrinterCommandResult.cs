using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class VerifyPrinterCommandResult
    {
        public bool IsInstalled { get; set; }
        public bool IsReady => 0 == Error.Length;
        public string PrinterName { get; set; }
        public string Error { get; set; }
    }
}
