using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    public class SessionManagerFactory : ISessionManagerFactory
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SessionManager> _logger;

        private readonly ConcurrentDictionary<Role, SessionManager> _sessionManagers = new ConcurrentDictionary<Role, SessionManager>();

        public SessionManagerFactory(IHttpClientFactory httpClientFactory, IUserRepository userRepository,
            ISessionRepository sessionRepository, ILogger<SessionManager> logger) => 
            (_httpClientFactory, _userRepository, _sessionRepository, _logger) = (httpClientFactory, userRepository, sessionRepository, logger);

        public SessionManager GetSessionManager(Role serverRole) 
        { 
            if (_sessionManagers.TryGetValue(serverRole, out SessionManager sessionManager))
                return sessionManager;
            lock (_sessionManagers) 
            {
                _sessionManagers[serverRole] = CreateSessionManager(serverRole);
                return _sessionManagers[serverRole];
            }
        }

        public IEnumerable<SessionManager> GetAllSessionManagers() 
        { 
            return _sessionManagers.Values;
        }

        public async Task ResetAllActiveSessions() 
        {
            await Task.WhenAll(_sessionManagers.Values.Select(sm => sm.ResetSession(true)));
        }

        private SessionManager CreateSessionManager(Role serverRole)
        {
            return new SessionManager(serverRole, _httpClientFactory, _userRepository, _sessionRepository, _logger);
        }
    }
}
