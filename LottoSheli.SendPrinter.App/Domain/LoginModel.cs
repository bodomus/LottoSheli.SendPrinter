using LottoSheli.SendPrinter.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.App.Domain
{
    internal class LoginModel
    {
        public ScannerMode ScannerMode { get; set; }
        public RightToLeft RightToLeft { get; set; }

        public string Password { get; set; }
        public string Message { get; set; }
        
    }

}
