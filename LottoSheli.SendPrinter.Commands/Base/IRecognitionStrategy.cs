using System;
using System.Collections.Generic;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.OCR.Base
{
    /// <summary>
    /// Provides description for proprocess image before recognition
    /// </summary>
    public interface IRecognitionPreprocessStrategy
    {
        /// <summary>
        /// Pre recognition options
        /// </summary>
        public struct PrerecognizeOptions
        {
            public PrerecognizeOptions()
            {
            }

            /// <summary>
            /// Image threshold
            /// </summary>
            public int ImageThreshold { get; init; } = 0;

            /// <summary>
            /// upscale/downscale ratio for specified image
            /// </summary>
            public double Ratio { get; init; } = 1;
        }

        /// <summary>
        /// Proprocess and precognizes specified image
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="image">specified image</param>
        /// <param name="decode">decode strategy</param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        T Recognize<T>(Bitmap image, PrerecognizeOptions options, Func<Bitmap, T> decode);
    }
}
