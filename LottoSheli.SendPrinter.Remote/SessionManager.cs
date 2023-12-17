using LottoSheli.SendPrinter.DTO.Remote;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    public class SessionManager : IConnectionStateInformer
    {
        public const string AUTH_HEADER = "X-CSRF-Token";
        public const string COOKIE_HEADER = "Cookie";
        public const string D9_VERSION_HEADER = "Printer-APP-Version";
        public const string D7_VERSION_HEADER = "Netappversion";
        private const int RETRY_DELAY = 3000;
        private const int SESSION_POLL_DELAY = 500;

        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SessionManager> _logger;
        private readonly Role _serverRole;

        private SessionInfo _session = new SessionInfo();
        private object _sessionLock = new object();
        private int _resetting = 0;
        private int _error = 0;
        private CancellationTokenSource _ctsrc = new CancellationTokenSource();

        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event EventHandler Connecting;
        public event EventHandler Error;

        public SessionInfo Session { get => _session; set => SetSession(value); }

        public bool HasSession => null != _session && !_session.IsEmpty;

        public CancellationToken SessionCancellationToken => _ctsrc.Token;

        public RemoteConnectionState ConnectionState =>
            HasSession
                ? RemoteConnectionState.Connected
                : _resetting > 0
                    ? RemoteConnectionState.Connecting
                    : _error > 0
                        ? RemoteConnectionState.Error
                        : RemoteConnectionState.Disconnected;
        public List<(string,string)> SessionHeaders => GetSessionHeaders(Session);

        public SessionManager(Role role, IHttpClientFactory httpClientFactory, IUserRepository userRepository, 
            ISessionRepository sessionRepository, ILogger<SessionManager> logger) 
        { 
            _serverRole = role;
            _httpClientFactory = httpClientFactory;
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _logger = logger;
            
        }

        public async Task<bool> TestCredentials(string url, string login, string password)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(url);
            var response = await SendLoginRequestAsync(_serverRole, client, SerializeCredentials(_serverRole, login, password), _ctsrc.Token);
            return response.IsSuccessStatusCode;
        }

        public Task EnsureSessionPresent() => HasSession ? Task.CompletedTask : ResetSession();

        public Task ResetSession() => ResetSession(false);

        public async Task ResetSession(bool force)
        {
            if (!force && (_resetting > 0 || HasSession))
            {
                await WaitForSession();
                return;
            }

            try
            {
                Interlocked.Exchange(ref _resetting, 1);
                Connecting?.Invoke(this, EventArgs.Empty);
                DropSession();
                CancelPendingRequests();
                
                while (!HasSession)
                {
                    await DoLoginAsync();
                    await Task.Delay(RETRY_DELAY);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"LOGIN ERROR: {ex.Message}");
            }
            finally
            {
                Interlocked.Exchange(ref _resetting, 0);
            }
        }

        public void InformRequestError(HttpRequestException reqEx =  null) 
        {
            Interlocked.Exchange(ref _error, 1);
            Error?.Invoke(this, EventArgs.Empty);
            if (reqEx?.IsSessionError() ?? false)
                _ = ResetSession(true);
        }

        public void InformRequestSuccess() 
        {
            Interlocked.Exchange(ref _error, 0);
        }

        private string GetStoredCredentials()
        {
            var serverCreds = _userRepository.GetByUserRole(_serverRole);
            if (serverCreds == null)
                throw new InvalidOperationException($"Cannot get credentials to login role {_serverRole}");

            var (login, password) = _userRepository.GetCredentials(_serverRole);
            return SerializeCredentials(_serverRole, login, password);
        }

        private async Task DoLoginAsync()
        {
            var client = _httpClientFactory.CreateClient(_serverRole.ToString());
            Uri uri = new Uri(client.BaseAddress, GetLoginPath(_serverRole));
            
            try
            {
                var response = await SendLoginRequestAsync(_serverRole, client, GetStoredCredentials(), _ctsrc.Token);
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();
                HandleLoginResponse(stringResponse);
            }
            catch (OperationCanceledException opCancel)
            {
                _logger.LogError($"LOGIN CANCELLED {uri} {opCancel.Message}");
            }
            catch (HttpRequestException reqEx)
            {
                HandleHttpException(reqEx, uri.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError($"LOGIN EXCEPTION {uri} {ex.Message}");
            }
        }

        private async Task WaitForSession()
        {
            do
                await Task.Delay(SESSION_POLL_DELAY);
            while (!HasSession);
        }

        private void DropSession()
        {
            lock (_session)
            {
                _session = new SessionInfo();
                if (_sessionRepository.HasSession(_serverRole)) 
                {
                    _sessionRepository.ClearSession(_serverRole);
                    Disconnected?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void SetSession(SessionInfo session)
        {
            lock (_session)
            {
                if (null != session && !session.IsEmpty) 
                {
                    _session = session;
                    _sessionRepository.UpsertSession(session);
                    Connected?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void CancelPendingRequests()
        {
            if (null != _ctsrc)
            {
                lock (_ctsrc) 
                {
                    _ctsrc.Cancel();
                    _ctsrc.Dispose();
                    _ctsrc = new CancellationTokenSource();
                }
            }
        }

        private void HandleLoginResponse(string responseStr)
        {
            if (string.IsNullOrEmpty(responseStr))
                return;
            var session = DeserializeSession(_serverRole, responseStr);
            SetSession(session);
            _logger.LogInformation($"LOGIN RESP: \n\ttoken: {session.Token},\n\tsession_name: {session.Name},\n\tsessid: {session.Value}");
        }

        private void HandleHttpException(HttpRequestException reqEx, string uri) 
        {
            if (HasSession && (HttpStatusCode.Forbidden == reqEx.StatusCode || HttpStatusCode.Unauthorized == reqEx.StatusCode))
            {
                DropSession();
            }
            var code = reqEx.StatusCode ?? HttpStatusCode.InternalServerError;
            string num = ((int)code).ToString();
            string text = code.ToString();
            _logger.LogError($"LOGIN {num} {text} {uri} {reqEx.Message}");
        }

        private HttpClient CreateClient() 
        { 
            var client = _httpClientFactory.CreateClient(_serverRole.ToString());
            if (client.DefaultRequestHeaders.Contains(AUTH_HEADER))
                client.DefaultRequestHeaders.Remove(AUTH_HEADER);
            return client;
        }

        public static Task<HttpResponseMessage> SendLoginRequestAsync(Role serverRole, HttpClient client, string loginData, CancellationToken ct) 
        {
            client.DefaultRequestHeaders.Clear();
            // specifilcly for D7 we add version header on login
            if (Role.D7 == serverRole) 
            {
                var (header, value) = GetVersionHeader(serverRole);
                client.DefaultRequestHeaders.Add(header, value);
            }
            
            var request = new HttpRequestMessage(HttpMethod.Post, GetLoginPath(serverRole));
            request.Content = new StringContent(loginData, Encoding.UTF8, "application/json");
            return client.SendAsync(request, ct);
        }

        public static string SerializeCredentials(Role serverRole, string login, string password) => serverRole switch
        {
            Role.D7 => $"{{\"data\": {{\"email\":\"{login}\", \"password\": \"{password}\"}}}}",
            Role.D9 => $"{{\"access_key\": \"{login}\"}}",
            _ => "{}"
        };

        public static string GetLoginPath(Role serverRole) => serverRole switch
        {
            Role.D7 => "/en/print_api/v1/login",
            Role.D9 => "/printer/login?_format=json",
            _ => string.Empty
        };


        // STATIC MEMBERS

        public static List<(string, string)> GetSessionHeaders(SessionInfo session) 
        {
            return null == session 
                ? new List<(string, string)>() 
                : new List<(string, string)>
                { 
                    new (AUTH_HEADER, session.Token),
                    new (COOKIE_HEADER, $"{session.Name}={session.Value}"),
                    GetVersionHeader(session.ServerType)
                };
        }

        public static SessionInfo DeserializeSession(Role serverRole, string serialized) 
        { 
            SessionInfo sessionInfo = new SessionInfo();
            sessionInfo.ServerType = serverRole;
            sessionInfo.Id = (int)serverRole;

            if (Role.D7 == serverRole)
            {
                D7SessionData d7Session = JsonConvert.DeserializeObject<ServerData<D7SessionData>>(serialized).Data;
                
                sessionInfo.Name = d7Session.SessionName;
                sessionInfo.Value = d7Session.SessionValue;
                sessionInfo.Token = d7Session.CsrfToken;
                sessionInfo.Session = JsonConvert.SerializeObject(d7Session);
            }
            else if (Role.D9 == serverRole) 
            {
                D9SessionData d9Session = JsonConvert.DeserializeObject<D9SessionData>(serialized);
                
                sessionInfo.Name = d9Session.Session?.Name ?? string.Empty;
                sessionInfo.Value = d9Session.Session?.Value ?? string.Empty;
                sessionInfo.Token = d9Session.CsrfToken;
                sessionInfo.Session = JsonConvert.SerializeObject(d9Session);
            }
            return sessionInfo;
        }

        public static (string, string) GetVersionHeader(Role serverRole) => serverRole switch 
        { 
            Role.D7 => new (D7_VERSION_HEADER, Assembly.GetExecutingAssembly().GetName().Version.ToString()),
            Role.D9 => new (D9_VERSION_HEADER, Assembly.GetExecutingAssembly().GetName().Version.ToString()),
            _ => throw new ArgumentException($"Unexpected server role: {serverRole}")
        };
    }
}
