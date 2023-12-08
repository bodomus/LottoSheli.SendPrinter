using LottoSheli.SendPrinter.Settings.Models;
using System.IO;
using System.Reflection;

namespace LottoSheli.SendPrinter.Settings.Adapters
{
    /// <summary>
    /// Contains properties for all printing/scaning settings in application managed for Miphal HaPais paper mode
    /// </summary>
    public class SettingsMiphalHapaisPaperAdapter : CommonPaperAdapter
    {
        protected override string _fileName => "MiphalHapaisPaper";

        public SettingsMiphalHapaisPaperAdapter(bool debug = false) : base(debug)
        {
        }
    }
}
