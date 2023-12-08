using System;

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
