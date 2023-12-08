using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.DTO.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Remote
{
    public interface IUpdateSummaryReportCommand : IParametrizedWithResultCommand<UpdateSummaryReportCommandData, Task<bool>>
    {
    }
}
