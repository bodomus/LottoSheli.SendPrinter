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
    /// <summary>
    /// Subscribes provided handler to remote clienyt events
    /// </summary>
    [Command(Basic = typeof(ISubscribeClientStateCommand))]
    public class SubscribeClientStateCommand : ISubscribeClientStateCommand
    {
        private readonly ISessionManagerFactory _sessionManagerFactory;

        public SubscribeClientStateCommand(ISessionManagerFactory sessionManagerFactory) 
        {
            _sessionManagerFactory = sessionManagerFactory;
        }

        public bool CanExecute()
        {
            return true;
        }

        public RemoteConnectionState Execute(SubscribeClientStateCommandData data)
        {
            var informer = _sessionManagerFactory.GetSessionManager(data.ServerRole);
            if (null != informer) 
            {
                informer.Connecting += (s, e) => data.StateHandler(informer.ConnectionState);
                informer.Connected += (s, e) => data.StateHandler(informer.ConnectionState);
                informer.Disconnected += (s, e) => data.StateHandler(informer.ConnectionState);
                informer.Error += (s, e) => data.StateHandler(informer.ConnectionState);
                return informer.ConnectionState;
            }
            return RemoteConnectionState.Disconnected;
        }
    }
}
