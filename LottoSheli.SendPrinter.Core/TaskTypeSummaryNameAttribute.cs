using System;

namespace LottoSheli.SendPrinter.Core
{
    public class TaskTypeSummaryNameAttribute : Attribute
    {
        public string Value;

        public TaskTypeSummaryNameAttribute(string value)
        {
            Value = value;
        }
    }

    public class TicketTypeAttribute : Attribute
    {
        public string Value;

        public TicketTypeAttribute(string value)
        {
            Value = value;
        }
    }
}
