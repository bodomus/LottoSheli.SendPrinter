using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
