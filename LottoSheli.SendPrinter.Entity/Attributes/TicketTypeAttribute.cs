using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity.Attributes
{
    public class TicketTypeAttribute : Attribute
    {
        public string Value;

        public TicketTypeAttribute(string value)
        {
            Value = value;
        }
    }
}
