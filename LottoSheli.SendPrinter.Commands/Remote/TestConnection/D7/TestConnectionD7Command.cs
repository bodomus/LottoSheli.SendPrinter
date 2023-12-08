using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.Remote;
using LottoSheli.SendPrinter.Remote;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.Tasks.Remote
{

    /// <summary>
    /// Resets remote connection
    /// </summary>
    [Command(Basic = typeof(ITestConnectionD7Command))]
    public class TestConnectionD7Command : ITestConnectionD7Command
    {
        private readonly ISessionManagerFactory _sessionManagerFactory;
        private readonly ILogger<ResetConnectionCommand> _logger;

        private class InternalMemoryStream : MemoryStream
        {
            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
            }
        }

        public TestConnectionD7Command(ISessionManagerFactory sessionManagerFactory, ILogger<ResetConnectionCommand> logger)
        {
            _sessionManagerFactory = sessionManagerFactory;
            _logger = logger;
        }

        public bool CanExecute()
        {
            return true;
        }

        public async Task<bool> Execute(TestConnectionCommandData data)
        {
            try
            {
                return await _sessionManagerFactory.GetSessionManager(Entity.Enums.Role.D7)
                    .TestCredentials(data.ServerUrl, data.Login, data.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
