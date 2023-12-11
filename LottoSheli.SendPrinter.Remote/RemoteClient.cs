using LottoSheli.SendPrinter.Core.Monitoring;
using LottoSheli.SendPrinter.DTO.Remote;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    public class RemoteClient : IRemoteClient
    {
        public const string RESILIENT = "_resilient";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISessionManagerFactory _sessionManagerFactory;
        private readonly ILogger<IRemoteClient> _logger;
        private readonly IMonitoringService _monitoringService;
        private CancellationTokenSource _ctsrc = new CancellationTokenSource();

        public RemoteClient(IHttpClientFactory httpClientFactory, ISessionManagerFactory sessionManagerFactory, IMonitoringService monitoringService, ILogger<IRemoteClient> logger) 
        { 
            _httpClientFactory = httpClientFactory;
            _sessionManagerFactory = sessionManagerFactory;
            _monitoringService = monitoringService;
            _logger = logger;
        }

        public void CancelPendingRequests()
        {
            if (null != _ctsrc)
            {
                _ctsrc.Cancel();
                _ctsrc.Dispose();
                _ctsrc = new CancellationTokenSource();
            }
        }

        [RemoteEndpoint(Role.D7, "/en/print_api/v1/exists", "GET")]
        public async Task<string> CheckTicketAsync(string slipId)
        {
            var pathExt = $"?slipid={slipId}";
            try
            {
                var response = await SendConfiguredRequestAsync(pathExt, string.Empty);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            { 
                return string.Empty;
            }
        }

        [RemoteEndpoint(Role.D9, "/api/printer/entity/printer-summary?_format=json", "POST")]
        public async Task<SummaryReportResponseData> CreateSummaryReport(SummaryReportData summaryReportData)
        {
            var reportData = JsonConvert.SerializeObject(summaryReportData);
            try 
            {
                var response = await SendConfiguredRequestAsync(string.Empty, reportData, true);
                var stringContent =  await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SummaryReportResponseData>(stringContent);
            } 
            catch 
            {
                return null;
            }
        }

        [RemoteEndpoint(Role.D9, "api/printer/printer-summary/ticket/date?_format=json", "PATCH")]
        public async Task<bool> UpdateSummaryReport(TicketData ticketData)
        {
            var json = JsonConvert.SerializeObject(ticketData);
            try
            {
                var response = await SendConfiguredRequestAsync(string.Empty, json, true);
                var stringContent = await response.Content.ReadAsStringAsync();
                return string.IsNullOrEmpty(stringContent);
            }
            catch
            {
                return false;
            }
        }

        [RemoteEndpoint(Role.D7, "/en/print_api/v1/draws", "GET")]
        public async Task<string> GetDrawsAsync()
        {
            try
            {
                var response = await SendConfiguredRequestAsync(string.Empty, string.Empty);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return string.Empty;
            }
        }

        [RemoteEndpoint(Role.D7, "/en/print_api/v1/task", "GET")]
        public async Task<string> GetPrintTask(TicketFilter filter, int limit)
        {
            string query = GetFilterQuery(filter, limit);
            try
            {
                var response = await SendConfiguredRequestAsync(query, string.Empty);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return string.Empty;
            }
        }

        [RemoteEndpoint(Role.D7, "/en/print_api/v1/get_win_tickets", "POST")]
        public async Task<string> GetWinners(int did, int stationId)
        {
            try
            {
                var winnersData = new ServerData<DTO.Remote.WinnersData> { Data = new DTO.Remote.WinnersData { DrawId = did } };
                if (stationId > 0)
                    winnersData.Data.StationId = stationId;
                string serialized = JsonConvert.SerializeObject(winnersData);
                var response = await SendConfiguredRequestAsync(string.Empty, serialized);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return string.Empty;
            }
        }

        [RemoteEndpoint(Role.D7, "/en/print_api/v1/task", "PUT")]
        public async Task SendPrintResultAsync(int id, PrintTaskResult payload)
        {
            try
            {
                string serialized = JsonConvert.SerializeObject(payload);
                await SendConfiguredRequestAsync($"/{id}", serialized, true);
            }
            catch
            {
                return;
            }
        }

        [RemoteEndpoint(Role.D7, "/en/print_api/v1/scan", "POST")]
        public Task<bool> SendScanResultAsync(TicketPayloadData payload, Stream stream)
        {
            throw new NotImplementedException();
        }

        
        private async Task<HttpResponseMessage> SendConfiguredRequestAsync(string pathExtension, string serializedContent, bool resilient = false, [CallerMemberName] string memberName = "") 
        {

            var epAttr = GetEndpointAttribute(memberName);
            if (epAttr is { ServerRole: var role, Path: var path, Verb: var verb })
            {
                var sessionManager = _sessionManagerFactory.GetSessionManager(role);
                await sessionManager.EnsureSessionPresent();

                var clientName = resilient 
                    ? $"{role}{RESILIENT}" 
                    : role.ToString();
                var client = _httpClientFactory.CreateClient(clientName);
                var uri = new Uri(client.BaseAddress, path + pathExtension);
                try
                {

                    Stopwatch timer = Stopwatch.StartNew();
                    var request = new HttpRequestMessage(new HttpMethod(verb), path + pathExtension);
                    if (!string.IsNullOrEmpty(serializedContent))
                        request.Content = new StringContent(serializedContent, Encoding.UTF8, "application/json");

                    using var ctsrc = CancellationTokenSource.CreateLinkedTokenSource(_ctsrc.Token, sessionManager.SessionCancellationToken);
                    var response = await client.SendAsync(request, ctsrc.Token);
                    timer.Stop();
                    await _monitoringService.InformRequestDuration((int)timer.ElapsedMilliseconds);
                    response.EnsureSuccessStatusCode();
                    sessionManager.InformRequestSuccess();
                    return response;
                }
                catch (HttpRequestException reqEx)
                {
                    sessionManager.InformRequestError(reqEx);
                    HandleHttpException(reqEx, uri.AbsoluteUri);
                    throw;
                }
                catch (OperationCanceledException opEx)
                {
                    HandleCancellationException(opEx, uri.AbsoluteUri);
                    throw;
                }
                catch (Exception ex) 
                {
                    sessionManager.InformRequestError();
                    HandleException(ex, uri.AbsoluteUri);
                    throw;
                }
                
            }

            throw new InvalidOperationException($"Endpoint configuration missing");
        }

        private RemoteEndpointAttribute GetEndpointAttribute(string memberName) 
        {
            var members = typeof(RemoteClient).GetMember(memberName);
            return members.Length > 0 ? members[0].GetCustomAttribute<RemoteEndpointAttribute>() : null;
        }

        private string GetFilterQuery(TicketFilter filter, int limit)
        {
            var result = new StringBuilder($"?limit={limit}");

            switch (filter)
            {
                case TicketFilter.Chance | TicketFilter.Regular:
                    result.Append("&game=chance&subtype=regular_chance");
                    break;
                case TicketFilter.Chance | TicketFilter.Multiple:
                    result.Append("&game=chance&subtype=multiple_chance");
                    break;
                case TicketFilter.Chance | TicketFilter.Methodical:
                    result.Append("&game=chance&subtype=methodical_chance");
                    break;
                case TicketFilter.Lotto | TicketFilter.Regular:
                    result.Append("&game=lotto&subtype=regular");
                    break;
                case TicketFilter.Lotto | TicketFilter.Regular | TicketFilter.Methodical:
                    result.Append("&game=lotto&subtype=methodical");
                    break;
                case TicketFilter.Lotto | TicketFilter.Regular | TicketFilter.Methodical | TicketFilter.Strong:
                    result.Append("&game=lotto&subtype=strong_methodical");
                    break;
                case TicketFilter.Lotto | TicketFilter.Double:
                    result.Append("&game=lotto&subtype=double");
                    break;
                case TicketFilter.Lotto | TicketFilter.Double | TicketFilter.Methodical:
                    result.Append("&game=lotto&subtype=double_methodical");
                    break;
                case TicketFilter.Lotto | TicketFilter.Double | TicketFilter.Methodical | TicketFilter.Strong:
                    result.Append("&game=lotto&subtype=double_strong_methodical");
                    break;
                case TicketFilter.SevenSevenSeven | TicketFilter.Regular:
                    result.Append("&game=777&subtype=777");
                    break;
                case TicketFilter.SevenSevenSeven | TicketFilter.Methodical:
                    result.Append("&game=777&subtype=777_methodical");
                    break;
                case TicketFilter.OneTwoThree | TicketFilter.Regular:
                    result.Append("&game=123&subtype=123");
                    break;
            };

            return result.ToString();
        }

        private void HandleHttpException(HttpRequestException reqEx, string uri)
        {
            var code = reqEx.StatusCode ?? HttpStatusCode.InternalServerError;
            _logger.LogError($"REMOTE {(int)code} {code} {uri} {reqEx.Message}");
        }

        private void HandleCancellationException(OperationCanceledException opEx, string uri)
        {
            _logger.LogError($"REMOTE CANCELLED {uri} {opEx.Message}");
        }

        private void HandleException(Exception ex, string uri)
        {
            _logger.LogError($"REMOTE INTERNAL {uri} {ex.Message}");
        }

        public SessionInfo GetSession(Role role)
        {
            return _sessionManagerFactory.GetSessionManager(role).Session;
        }

        public SessionManager GetSessionManager(Role role)
        {
            return _sessionManagerFactory.GetSessionManager(role);
        }

        public Task Reconnect(Role role)
        {
            return GetSessionManager(role).ResetSession(true);
        }

        private bool VerbIsOutgoing(string verb) => 
            verb.ToUpperInvariant() == "POST" || verb.ToUpperInvariant() == "PUT";

        
    }
}
