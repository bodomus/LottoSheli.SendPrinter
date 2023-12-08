using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Remote;
using LottoSheli.SendPrinter.Remote;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Tasks.Remote
{

    /// <summary>
    /// Resets remote connection
    /// </summary>
    [Command(Basic = typeof(IResetConnectionCommand))]
    public class ResetConnectionCommand : IResetConnectionCommand
    {
        private readonly ISessionManagerFactory _sessionManagerFactory;

        private class InternalMemoryStream : MemoryStream
        {
            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
            }
        }

        public ResetConnectionCommand(ISessionManagerFactory sessionManagerFactory)
        {
            _sessionManagerFactory = sessionManagerFactory;
        }

        public bool CanExecute()
        {
            return true;
        }

        public async Task Execute()
        {
            await _sessionManagerFactory.ResetAllActiveSessions();
        }
    }
}
