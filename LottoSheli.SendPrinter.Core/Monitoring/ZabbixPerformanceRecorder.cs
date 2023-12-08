using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZabbixApi;
using ZabbixApi.Entities;

namespace LottoSheli.SendPrinter.Core.Monitoring
{
    public interface IZabbixPerformanceRecorder : IPerformanceRecorder 
    { 
        string ZabbixUrl { get; set; }
        string StationId { get; set; }
    }

    public class ZabbixData 
    {
        public string request { get; set; } = "sender data";
        public List<ZabbixKeyValue> data { get; set; }
    }

    public class ZabbixKeyValue 
    { 
        public string host { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }

    public class ZabbixPerformanceRecorder : IZabbixPerformanceRecorder, IDisposable
    {
        private const int ZBX_PORT = 10051;
        private bool _disposed;
        private readonly ILogger<ZabbixPerformanceRecorder> _logger;
        private readonly Dictionary<string, string> _keys = new Dictionary<string, string> 
        {
            { "netcli.cpu", "CPUUsage" },
            { "netcli.mem", "MemoryUsage"},
            { "netcli.handles", "HandlesUsed"},
            { "netcli.printed", "TicketsPrinted"},
            { "netcli.sent", "TicketsSent"},
            { "netcli.ard", "AverageRequestDuration"},
        };

        private string _apiUrl => $"https://zabbix.lottosheli.co.il/zabbix/api_jsonrpc.php";
        
        public string StationId { get; set; } = "000000";
        public string ZabbixUrl { get; set; } = "172.31.41.41";
        public string Host => $"netcli-{StationId}";

        public ZabbixPerformanceRecorder(ILogger<ZabbixPerformanceRecorder> logger) 
        { 
            _logger = logger;
        }

        public void Record(PerformanceRecord record)
        {
            _ = RecordAsync(record);
        }

        public async Task RecordAsync(PerformanceRecord record)
        {
            try
            {
                var zdata = CreateZabbixData(record);
                var json = JsonConvert.SerializeObject(zdata);
                // TODO Remove comment line 
                //var response = await SendData(json);
                //_logger.LogInformation($"ZBX sent {record}, response={response}");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private ZabbixData CreateZabbixData(PerformanceRecord record) 
        {
            var props = record.GetType().GetProperties();
            var data = _keys.Select(kv =>
            {
                var prop = record.GetType().GetProperty(kv.Value);

                return new ZabbixKeyValue
                {
                    host = Host,
                    key = kv.Key,
                    value = prop?.GetValue(record)?.ToString() ?? null
                };
            }).ToList();
            return new ZabbixData { data = data };
        }

        private Task<string> SendData(string json) 
        {
            return Task.Run(() => 
            {
                byte[] header = Encoding.ASCII.GetBytes("ZBXD\x01");
                byte[] length = BitConverter.GetBytes((long)json.Length);
                byte[] data = Encoding.ASCII.GetBytes(json);

                byte[] all = new byte[header.Length + length.Length + data.Length];

                Buffer.BlockCopy(header, 0, all, 0, header.Length);
                Buffer.BlockCopy(length, 0, all, header.Length, length.Length);
                Buffer.BlockCopy(data, 0, all, header.Length + length.Length, data.Length);

                using (var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    client.Connect(ZabbixUrl, ZBX_PORT);
                    client.Send(all);

                    // header

                    byte[] buffer = new byte[5];
                    ReceiveData(client, buffer, 0, buffer.Length, 10000);

                    if ("ZBXD\x01" != Encoding.ASCII.GetString(buffer, 0, buffer.Length))
                        throw new Exception("Invalid response");


                    // message length

                    buffer = new byte[8];
                    ReceiveData(client, buffer, 0, buffer.Length, 10000);
                    int dataLength = BitConverter.ToInt32(buffer, 0);

                    if (dataLength == 0)
                        throw new Exception("Invalid response");


                    // message body

                    buffer = new byte[dataLength];
                    ReceiveData(client, buffer, 0, buffer.Length, 10000);

                    return Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                }
            });
        }

        private void ReceiveData(Socket socket, byte[] buffer, int offset, int size, int timeout)
        {
            int startTickCount = Environment.TickCount;
            int received = 0;
            do
            {
                if (Environment.TickCount > startTickCount + timeout)
                    throw new Exception("Timeout.");
                try
                {
                    received += socket.Receive(buffer, offset + received, size - received, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.WouldBlock ||
                        ex.SocketErrorCode == SocketError.IOPending ||
                        ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                    {
                        // socket buffer is probably empty, wait and try again
                        Thread.Sleep(30);
                    }
                    else
                        throw ex;  // any serious error occurr
                }
            } while (received < size);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                   
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
