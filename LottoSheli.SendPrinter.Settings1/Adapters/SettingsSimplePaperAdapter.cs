using LottoSheli.SendPrinter.Settings.Models;
using System;
using System.IO;
using System.Reflection;

namespace LottoSheli.SendPrinter.Settings.Adapters
{
    /// <summary>
    /// Contains properties for all printing/scaning settings in application managed for simple paper mode
    /// </summary>
    public class SettingsSimplePaperAdapter : CommonPaperAdapter
    {
        protected override string _fileName => "SimplePaper";

        public SettingsSimplePaperAdapter(bool debug = false) : base(debug)
        {
        }
    }
}
