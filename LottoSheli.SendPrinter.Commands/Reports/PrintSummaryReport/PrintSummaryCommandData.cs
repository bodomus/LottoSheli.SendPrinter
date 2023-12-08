using LottoSheli.SendPrinter.Commands.Base;
using System;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class PrintSummaryCommandData : ICommandData
    {
        public IList<Tuple<int, int>> Ranges { get; set; } = new List<Tuple<int, int>>();

        public void AddRange(int start, int end) 
        { 
            if (null == Ranges)
                Ranges = new List<Tuple<int, int>>();
            Ranges.Add(new Tuple<int, int>(start, end));
        }
    }
}
