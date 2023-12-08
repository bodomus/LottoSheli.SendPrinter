using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core
{
    /// <summary>
    /// Thread-safe sequence number generator service
    /// </summary>
    ///
    public class SequenceService : ISequenceService
    {
        private int _current = -1;
        public int Current { get => _current; set => Interlocked.Exchange(ref _current, (value % int.MaxValue)); }
        public int Next => GetNext();
        public void Reset() => Interlocked.Exchange(ref _current, -1);

        private int GetNext() 
        {
            if (int.MaxValue == _current)
                Reset();
            return Interlocked.Increment(ref _current);
        }
    }
}
