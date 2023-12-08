using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Remote;

namespace LottoSheli.SendPrinter.Commands.Remote
{
    public interface ISubscribeClientStateCommand : IParametrizedWithResultCommand<SubscribeClientStateCommandData, RemoteConnectionState>
    {
    }
}
