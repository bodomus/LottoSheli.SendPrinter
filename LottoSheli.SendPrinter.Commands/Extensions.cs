using LottoSheli.SendPrinter.Commands.Print;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ZXing;

namespace LottoSheli.SendPrinter.Commands
{
    /// <summary>
    /// Provides extension methods for this assembly.
    /// </summary>
    internal static class Extensions
    {

        /// <summary> to decode a barcode within an image which is given by a <see cref="Bitmap"/>. 
        /// That method gives a chance to prepare a luminance source completely before calling the time consuming decoding method. 
        /// On the other hand there is a chance to create a <see cref="Bitmap"/> and the decoding call can be made in a background thread.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="source">The bitmap.</param>
        /// <returns></returns>
        public static Result Decode(this IBarcodeReader reader, Bitmap source)
        {
            BitmapLuminanceSource luminanceSource = new BitmapLuminanceSource(source);

            return reader.Decode(luminanceSource);
        }


        /// <summary>
        /// fill dictionary with indices of elements to be rendered on a ticket
        /// </summary>
        /// <param name="indices">dictionary of indices to fill</param>
        public static void FillCommonElements(this CreateLottoTicketImageCommandData data, Dictionary<string, List<int>> indices)
        {
            var tables = data.ExisintgTicketTask.Tables.OrderBy(obj => obj.Index).ToList();

            for (int i = 0; i < tables.Count; i++)
            {
                var groupIndices = new List<int>();
                string groupName = $"num{i + 1}";

                indices.Add(groupName, groupIndices);

                var table = tables[i];
                groupIndices.AddRange(table.Numbers.Select(number => (int)int.Parse(Convert.ToString(number)) - 1));
            }

            if (data.ExisintgTicketTask.MultipleDraw >= 2 && data.ExisintgTicketTask.MultipleDraw <= 4)
            {
                indices.Add("multiple_draw", new List<int> { data.ExisintgTicketTask.MultipleDraw - 2 });
            }
        }

        /// <summary>
        /// Returns full bitmap rectangle for specified <see cref="Image"/>
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Rectangle GetImageRectangle(this Image img) => new(Point.Empty, img.Size);
    }
}
