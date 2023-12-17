using LottoSheli.SendPrinter.Core.Monitoring;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Repository;
using LottoSheli.SendPrinter.Settings;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    public class SendingQueue : ISendingQueue, IDisposable
    {
        public const string HTTP_CLIENT_NAME = "SendScan";
        public const string SCAN_URI = "/en/print_api/v1/scan";
        public const string TOKEN_HEADER = "X-CSRF-Token";

        private const int DEFAULT_CONCURRENCY = 10;
        private const int REQUEUE_DELAY = 3000;
        private const int DURATION_SAMPLE_COUNT = 5;
       
        private const RecognitionJobStatus STATUS_SENDING = RecognitionJobStatus.Recognized | RecognitionJobStatus.Matched | RecognitionJobStatus.Sending;
        private const RecognitionJobStatus STATUS_SENT = RecognitionJobStatus.Recognized | RecognitionJobStatus.Matched | RecognitionJobStatus.Sent;
        private const RecognitionJobStatus STATUS_NOT_SENT = RecognitionJobStatus.Recognized | RecognitionJobStatus.Matched | RecognitionJobStatus.NotSent;

        private ConcurrentQueue<RecognitionJob> _queue = new ConcurrentQueue<RecognitionJob>();
        private ConcurrentBag<RecognitionJob> _sendingJobs = new ConcurrentBag<RecognitionJob>();
        private int _count = 0;
        private int _stopped = 1;
        
        private CancellationTokenSource _ctsrc = new CancellationTokenSource();
        private SemaphoreSlim _throttler;
        private readonly IRecognitionJobRepository _jobRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMonitoringService _monitoringService;
        private readonly ILogger<ISendingQueue> _logger;
        private readonly ISettings _settings;
        private readonly SessionManager _auth;
        private readonly List<int> _durationSamples = new List<int>();

        public event EventHandler<SendingQueueEventArgs> JobEnqueuedForSending;
        public event EventHandler<SendingQueueEventArgs> JobSendingStarted;
        public event EventHandler<SendingQueueEventArgs> JobSendingFinished;

        public bool IsIdle => _throttler.CurrentCount == DEFAULT_CONCURRENCY;
        public bool IsStopped => _stopped > 0;
        public bool IsSessionPresent => _auth.HasSession;
        public int ActiveWorkerCount => DEFAULT_CONCURRENCY - _throttler.CurrentCount;
        public int AverageSendingDuration => (int)Math.Round(_durationSamples.Count > 0 ? _durationSamples.Average() : 0);
        public int TotalJobCount => _queue.Count + _sendingJobs.Count;
        public IEnumerable<RecognitionJob> PendingJobs => _queue.AsEnumerable();
        public IEnumerable<RecognitionJob> SendingJobs => _sendingJobs.AsEnumerable();

        public SendingQueue(IRecognitionJobRepository jobRepository,
            IHttpClientFactory httpClientFactory,
            ISessionManagerFactory sessionManagerFactory, 
            IMonitoringService monitoringService,
            ISettings settings,
            ILogger<ISendingQueue> logger)
        {
            (_jobRepository, _httpClientFactory, _monitoringService, _settings, _logger) = 
                (jobRepository, httpClientFactory, monitoringService, settings, logger);
            _auth = sessionManagerFactory.GetSessionManager(Role.D7);
            _throttler = new SemaphoreSlim(DEFAULT_CONCURRENCY);
        }
            

        public async void Start()
        {
            if (0 == _stopped) return;

            Interlocked.Exchange(ref _stopped, 0);
            await _auth.EnsureSessionPresent();
            await LoadStoredJobs();
        }

        public void Stop()
        {
            if (_stopped > 0) return;

            Interlocked.Exchange(ref _stopped, 1);
            _queue.Clear();
            
            if (null != _ctsrc)
            {
                _ctsrc.Cancel();
                _ctsrc.Dispose();
                _ctsrc = new CancellationTokenSource();
            }
        }

        public void StartJob(RecognitionJob job)
        {
            if (IsValidJob(job))
            {
                SetJobStatus(job, STATUS_SENDING);
                if (!_queue.Contains(job))
                    Enqueue(job);
            }
        }

        public void StartAllJobs() 
        {
            foreach (var job in GetJobsReadyForSending())
                if (job.JobStatus.Equals(STATUS_NOT_SENT))
                    StartJob(job);
        }

        public void StopJob(RecognitionJob job)
        {
            SetJobStatus(job, STATUS_NOT_SENT);
        }

        public void StopAllJobs()
        {
            foreach (var job in GetJobsReadyForSending())
                if (job.JobStatus.Equals(STATUS_SENDING))
                    StopJob(job);
        }

        private void Enqueue(RecognitionJob job)
        {
            if (job.JobStatus.Equals(STATUS_SENT)) 
            {
                HandleCompletedJob(job, $"SCAN SENT BEFORE job={job.Guid} job status={job.JobStatus}");
                return;
            }
            if (!_queue.Contains(job))
                _queue.Enqueue(job);
            JobEnqueuedForSending?.Invoke(this, CreateEventArgs(job));
            if (!IsStopped && _throttler.CurrentCount > 0) 
            {
                _ = DoSending();
            }
        }

        private async Task Requeue(RecognitionJob job) 
        {
            await Task.Delay(REQUEUE_DELAY);
            Enqueue(job);
        }

        private async Task DoSending()
        {
            var ct = _ctsrc.Token;
            await Task.Factory.StartNew(async () =>
            {
                await _throttler.WaitAsync();
                while (_queue.TryDequeue(out RecognitionJob job))
                {
                    if (IsStopped)
                        break;
                        
                    // not sent status serves as a marker of job that should not be sent for now    
                    if (STATUS_NOT_SENT.Equals(job.JobStatus))
                        continue;

                    Stopwatch sw = new Stopwatch();
                    JobSendingStarted?.Invoke(this, CreateEventArgs(job));
                    try
                    {
                        ct.ThrowIfCancellationRequested();

                        sw.Start();
                        if (!_sendingJobs.Contains(job))
                            _sendingJobs.Add(job);
                        await Send(job);
                    }
                    catch (OperationCanceledException) 
                    {
                        _logger.LogWarning($"SCANQUEUE: sending cancelled");
                        await Requeue(job);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"SCANQUEUE {ex.Message}\r\n{ex.StackTrace}");
                        await Requeue(job);
                    }
                    finally
                    {
                        sw.Stop();
                        _sendingJobs.TryTake(out RecognitionJob sjob);
                        _throttler.Release();
                        if (STATUS_SENT == job.JobStatus)
                            HandleCompletedJob(job, $"SCAN SENT: ticket {job.Ticket.Id} {sw.ElapsedMilliseconds}ms");
                    }
                }
            }, ct, TaskCreationOptions.LongRunning | TaskCreationOptions.DenyChildAttach, TaskScheduler.Default).Unwrap();
        }

        private async Task Send(RecognitionJob job)
        {
            try
            {
                var payload = CreatePayload(job);
                using var image = await PrepareScanImage(job);
                var response = await SendScanAsync(payload, image, Path.GetFileName(job.ImagePath));
                await HandleServerResponse(job, response);
            }
            catch (HttpRequestException rex)
            {
                _auth.InformRequestError(rex);
                await HandleServerResponse(job, rex);
            }
            catch (Exception)
            {
                _auth.InformRequestError();
                throw;
            }
        }

        private async Task LoadStoredJobs()
        {
            foreach (var job in GetJobsReadyForSending())
                if (job.JobStatus.Equals(STATUS_SENDING))
                {
                    await Task.Delay(REQUEUE_DELAY / 10);
                    Enqueue(job);
                }
        }

        private Task<Bitmap> PrepareScanImage(RecognitionJob job)
        {
            throw new NotImplementedException();
            //var ct = _ctsrc.Token;
            //return Task.Run(() =>
            //{
            //    ct.ThrowIfCancellationRequested();
            //    using Bitmap scan = job.Scan;
            //    if (null == scan)
            //        throw new InvalidOperationException($"Bitmap expected for sending. Got NULL");
            //    var gdb = new GrayDividingBlender();
            //    var otsu = new OtsuThreshold();
            //    using Bitmap afterBlender = gdb.Apply(scan);
            //    return otsu.Apply(afterBlender);
            //}, ct);
        }

        private TicketPayloadData CreatePayload(RecognitionJob job)
        {
            if (null == job.Ticket)
                throw new InvalidOperationException($"Trying create payload without matched ticket");

            bool isLotto = GameType.Lotto == job.RecognizedData.GameType;
            return new TicketPayloadData
            {
                CreatedDate = DateTime.Now,
                Sequence = job.Ticket.Sequence,
                Id = job.Ticket.Id,
                Guid = job.Guid,
                Data = new ScannerResponseData
                {
                    Barcode = new TicketBarcodeData
                    {
                        Extra = isLotto ? ParseExtra(job.RecognizedData.Extra) : null,
                        ExtraMark = isLotto ? (int)job.RecognizedData.ExtraMark : null,
                        Tables = job.Ticket.Tables,
                        UserId = job.Ticket.UserId,
                    },
                    CropLine = job.RecognizedData.TopCropline,
                    BottomCropLine = job.RecognizedData.BottomCropline,
                    SlipId = job.RecognizedData.SlipId,
                    Status = 1,
                    Tid = job.Ticket.TicketId,
                    SkipUpdate = false
                }
            };
        }

        private async Task<HttpResponseMessage> SendScanAsync(TicketPayloadData payload, Bitmap scan, string fname)
        {
            await _auth.EnsureSessionPresent();

            using HttpClient client = _httpClientFactory.CreateClient(HTTP_CLIENT_NAME);

            using var form = new MultipartFormDataContent();
            using var ms = new MemoryStream();
            scan.Save(ms, ImageFormat.Png);
            byte[] bytes = ms.ToArray();
            var imgContent = new ByteArrayContent(bytes, 0, bytes.Length);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(payload.Data), Encoding.UTF8, "application/json");
            
            form.Add(imgContent, "file", fname);
            form.Add(jsonContent, "data");
            Stopwatch timer = Stopwatch.StartNew();
            var response = await client.PostAsync(SCAN_URI, form, _auth.SessionCancellationToken);
            timer.Stop();
            int msec = (int)timer.ElapsedMilliseconds;
            await _monitoringService.InformRequestDuration(msec);
            RecordDuration(msec);
            return response;
        }

        private int[] ParseExtra(string extra) => string.IsNullOrEmpty(extra) 
            ? new int[0] 
            : extra.Split("")
                .Select(x => int.TryParse(x, out int res) ? res : -1)
                .ToArray();

        private bool IsValidJob(RecognitionJob job) =>
            job.JobStatus.HasFlag(RecognitionJobStatus.Recognized) 
            && job.JobStatus.HasFlag(RecognitionJobStatus.Matched) 
            && !job.JobStatus.HasFlag(RecognitionJobStatus.Sent);

        private IEnumerable<RecognitionJob> GetJobsReadyForSending() => _jobRepository.GetAll().Where(job => IsValidJob(job));

        private void SetJobStatus(RecognitionJob job, RecognitionJobStatus status) 
        {
            var oldStatus = job.JobStatus;
            if (oldStatus != status) 
            { 
                job.JobStatus = status;
                _jobRepository.Update(job);
            }
        }

        private async Task HandleServerResponse(RecognitionJob job, HttpResponseMessage response) 
        {
            try
            {
                if (IsServerErrorResponse(response)) 
                    await InformInternalServerError(response);

                job.SendStatus = (int)response.StatusCode;
                response.EnsureSuccessStatusCode();
                _auth.InformRequestSuccess();
                await _monitoringService.InformTicketSent();
                Interlocked.Exchange(ref _count, _count + 1);
                SetJobStatus(job, STATUS_SENT);
            }
            catch (HttpRequestException reqEx) 
            { 
                await HandleServerResponse(job, reqEx);
            }
        }
        private async Task HandleServerResponse(RecognitionJob job, HttpRequestException error)
        {
            _logger.LogError($"SCANQUEUE {error.StatusCode} {error.Message}\r\n{error.StackTrace}");
            await Requeue(job);
        }

        private void HandleCompletedJob(RecognitionJob job, string logMessage)
        {
            JobSendingFinished?.Invoke(this, CreateEventArgs(job));
            _logger.LogInformation(logMessage);
        }

        private bool IsServerErrorResponse(HttpResponseMessage response) => 5 <= ((int)response.StatusCode / 100);

        private void RecordDuration(int msec) 
        { 
            _durationSamples.Add(msec);
            if (_durationSamples.Count > DURATION_SAMPLE_COUNT)
                _durationSamples.RemoveAt(0);
        }

        private async Task InformInternalServerError(HttpResponseMessage response) 
        {
            var content = await response.Content.ReadAsStringAsync();
            var headers = response.Headers.Select(kv => $"{kv.Key}: {string.Join(",", kv.Value.ToArray())}").ToArray();
            var headersString = string.Join("\r\n", headers);
            _logger.LogError($"SCANQUEUE ERROR {response.ReasonPhrase}\r\n{headersString}\r\n{content}");
        }

        private SendingQueueEventArgs CreateEventArgs(RecognitionJob job) =>
            new SendingQueueEventArgs
            {
                Job = job,
                Sending = _sendingJobs.Count,
                Pending = _queue.Count,
                Sent = _count
            };

        public void Dispose()
        {
            if (null != _ctsrc)
            {
                _ctsrc.Cancel();
                _ctsrc.Dispose();
            }
        }
    }
}
