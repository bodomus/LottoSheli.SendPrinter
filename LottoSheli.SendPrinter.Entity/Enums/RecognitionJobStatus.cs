using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity.Enums
{
    /// <summary>
    /// Complete job status consists of trenary results of 3 main ops: recognize, match, send
    /// </summary>
    [Flags]
    public enum RecognitionJobStatus
    {
        NotRecognized = 1,
        RecognizedWithErrors = 2,
        Recognized = 4,
        NotMatched = 8,
        MatchedPartially = 16,
        Matched = 32,
        NotSent = 64,
        Sending = 128,
        Sent = 256,
        Initial = NotRecognized | NotMatched | NotSent,
        AllDone = Recognized | Matched | Sent,
        Any = 512,
        Duplicate = 1024
    }
}
