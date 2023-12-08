using LottoSheli.SendPrinter.Commands.Base;
using System;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Commands.Print
{
    public class PrintBarcodeCommandData : ICommandData
    {
        public IList<Tuple<int, int>> Ranges { get; init; } 
        public int[] Bundles { get; init; }
    }
}
