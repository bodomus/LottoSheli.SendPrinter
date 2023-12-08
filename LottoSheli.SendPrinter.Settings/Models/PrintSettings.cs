using System;
using System.Collections.Generic;
using System.Text;

namespace LottoSheli.SendPrinter.Settings.Models
{
    ///// <summary>
    ///// printer specific settings
    ///// </summary>
    public class PrintSettings : ICloneable
    {
        public string ProfileKey;
        /// <summary>
        /// offset from left edge
        /// </summary>
        public float PrintHorOffset;

        /// <summary>
        /// offset from top edge
        /// </summary>
        public float PrintVertOffset;

        /// <summary>
        /// print image width
        /// </summary>
        public float PrintWidth;

        /// <summary>
        /// print image height
        /// </summary>
        public float PrintHeight;

        /// <summary>
        /// \deprecated. Used earlier when process was automated
        /// </summary>
        public bool PrintFlipX;

        /// <summary>
        /// \deprecated. Used earlier when process was automated
        /// </summary>
        public bool PrintFlipY;

        /// <summary>
        /// used in old printing mode, when printing was made on paper roll
        /// signalized end of ticket
        /// </summary>
        public float BottomLineOffset;

        /// <summary>
        /// width of ticket end line
        /// used in old printing mode
        /// </summary>
        public float BottomLineWidth;

        public object Clone()
        {
            return new PrintSettings {ProfileKey = ProfileKey,
                    PrintHorOffset = PrintHorOffset,
                    PrintVertOffset = PrintVertOffset,
                    PrintWidth = PrintWidth,
                    PrintHeight = PrintHeight,
                    PrintFlipX = PrintFlipX,
                    PrintFlipY = PrintFlipY,
                    BottomLineOffset = BottomLineOffset,
                    BottomLineWidth = BottomLineWidth
            };
        }
    }
}
