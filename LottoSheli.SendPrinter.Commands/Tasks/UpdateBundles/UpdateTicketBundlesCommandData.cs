using LottoSheli.SendPrinter.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    public class UpdateTicketBundlesCommandData : ICommandData
    {
        public IList<Tuple<int, int>> Ranges { get; set; }
    }
}
