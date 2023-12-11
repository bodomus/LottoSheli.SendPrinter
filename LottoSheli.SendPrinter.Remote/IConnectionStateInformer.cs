using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    public interface IConnectionStateInformer
    {
        event EventHandler Connected;
        event EventHandler Disconnected;
        event EventHandler Connecting;
        event EventHandler Error;

        RemoteConnectionState ConnectionState { get; }
    }
}
