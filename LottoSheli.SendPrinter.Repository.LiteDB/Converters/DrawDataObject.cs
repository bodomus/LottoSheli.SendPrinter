using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository.LiteDB.Converters
{
    class DrawDataObject
    {
        public IList<Draw> Game { get; set; }
    }
}
