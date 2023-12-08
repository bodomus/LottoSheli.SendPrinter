using System;
using System.Collections.Generic;
using System.Text;

namespace LottoSheli.SendPrinter.Settings.Models
{
    /// <summary>
    /// Extends <see cref="PrintSettings"/> and adds ticket tables count 
    /// </summary>
    public class TablePrintSettings : PrintSettings
    {
        /// <summary>
        /// Ticket Tables Count
        /// </summary>
        public int? TablesCount { get; set; }

        public override string ToString()
        {
            return ProfileKey;
        }

        public float PaperWidth { get; set; }

        public float PaperHeight { get; set; }

        /// <summary>
        /// Datermines that templated heihgt will be set automatically based on aspects ratio 
        /// </summary>
        public bool AutoPrintHeight { get; set; }

        public float BarcodeVerticalOffset { get; set; }

        public float BarcodeHorizontalOffset { get; set; }
    }
}
