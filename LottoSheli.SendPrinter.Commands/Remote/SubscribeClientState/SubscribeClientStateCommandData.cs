using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Remote
{
    public class SubscribeClientStateCommandData : ICommandData
    {
        public Func<RemoteConnectionState, bool> StateHandler { get; set; }
        public Role ServerRole { get; set; } = Role.D7;
    }
}
