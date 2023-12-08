using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity.Enums
{
    public enum TicketMatchingError
    {
        None = 0,
        NationalId = 1,
        Price = 2,
        Tables = 4,
        Subtype = 8,
        Type = 16,
        Unmatched = 32,
        Duplicate = 64
    }
}
