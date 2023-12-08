using System;


namespace LottoSheli.SendPrinter.Entity.Attributes
{
    public class TaskTypeSummaryNameAttribute : Attribute
    {
        public string Value;

        public TaskTypeSummaryNameAttribute(string value)
        {
            Value = value;
        }
    }
}
