using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    public interface IGetBundledTicketsCommand : IParametrizedWithResultCommand<GetBundledTicketsCommandData, IEnumerable<TicketTask>>
    {
    }
}
