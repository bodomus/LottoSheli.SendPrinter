using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Settings.Models
{
    public class WinnerSettings
    {
        public IEnumerable<TablePrintSettings> TableProfiles { get; set; }

        public string PrinterName { get; set; }
        public float PaperWidth { get; set; }
        public float PaperHeight { get; set; }
        public float HorizontalOffset { get; set; }
        public float VerticalOffset { get; set; }
        public bool GenerateRandomTables { get; set; } = false;
        public int MaxTemplateVariants { get; set; } = 5;
    }
}
