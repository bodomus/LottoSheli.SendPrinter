using LottoSheli.SendPrinter.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public interface IVerifyPrinterCommand : IResultCommand<VerifyPrinterCommandResult>
    {
    }
}
