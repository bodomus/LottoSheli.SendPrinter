using AForge.Imaging.Filters;
using LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Repository;
using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.SlipReader;
using LottoSheli.SendPrinter.SlipReader.Decoder;
using LottoSheli.SendPrinter.SlipReader.Template;
using LottoSheli.SendPrinter.SlipReader.Trainer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow
{
    public class RecognitionJobWorker : IRecognitionJobWorker
    {
        private string WINNERS_DIR => Path.Combine(SettingsManager.LottoHome, @"winners");
        private TimeSpan WINNERS_TTL = TimeSpan.FromDays(30);
        private int _retiring = 0;
        private int _running = 0;
        private RecognitionJobResults _jobResults = new RecognitionJobResults();

        public RecognitionJob Job { get; private set; }
        public IRecognitionJobCommand RecognizeCommand { get; set; }
        public IRecognitionJobCommand MatchCommand { get; set; }
        public IRecognitionJobCommand SendCommand { get; set; }
        public IRecognitionJobCommand CheckCommand { get; set; }
        public IRecognitionJobRepository Repository { get; init; }
        public ITicketTaskRepository TicketRepository { get; init; }
        public IScannedSlipRepository SlipRepository { get; init; }
        public ITrainDataService TrainDataService { get; init; }
        public ISlipBlockDecoderFactory SlipBlockDecoderFactory { get; init; }
        public IWinnersTemplateProvider WinnersTemplateProvider { get; init; }

        public RecognitionJobStatus Status 
        { 
            get => _jobResults.JobStatus; 
            set => _jobResults.JobStatus = value; 
        }

        public ScannedSlip ScannedSlip { get; set; }

        public bool IsReadyForMatching => 
            (Status.HasFlag(RecognitionJobStatus.Recognized) || Status.HasFlag(RecognitionJobStatus.RecognizedWithErrors));
        public bool IsReadyForSending => 
            Status.HasFlag(RecognitionJobStatus.Recognized) 
            && Status.HasFlag(RecognitionJobStatus.Matched) 
            && !Status.HasFlag(RecognitionJobStatus.Sent);
        public bool IsDuplicate => Status.HasFlag(RecognitionJobStatus.Duplicate);
        public bool IsCompletedSuccessfully => Status.HasFlag(RecognitionJobStatus.AllDone);
        public bool IsRetiring => _retiring > 0;
        public bool IsRunning => _running > 0;
        public bool BreaksOnDuplicate { get; set; } = true;
        public bool CollectTrainingData { get; set; } = false;
        public bool AutoupdateWinnersTemplates { get; set; } = false;
        public int WinnerTemplateVariants { get; set; } = 1;

        public SlipTemplate Template
        {
            get
            {
                return RecognizeCommand is IRecognizeCommand irc
                    ? irc.Template
                    : null;
            }
            set
            {
                if (RecognizeCommand is IRecognizeCommand irc)
                    irc.Template = value;
            }
        }


        public event EventHandler<RecognitionJob> JobStarted;
        public event EventHandler<RecognitionJob> JobFinished;
        public event EventHandler<RecognitionJob> JobStatusChanged;
        public event EventHandler<RecognitionJob> Recognizing;
        public event EventHandler<RecognitionJob> DoneRecognizing;
        public event EventHandler<RecognitionJob> Matching;
        public event EventHandler<RecognitionJob> DoneMatching;
        public event EventHandler<RecognitionJob> Sending;
        public event EventHandler<RecognitionJob> DoneSending;
        public event EventHandler<RecognitionJob> Checking;
        public event EventHandler<RecognitionJob> DoneChecking;
        public event EventHandler<RecognitionJob> Retired;
        public event EventHandler<RecognitionJob> Duplicate;

        public RecognitionJobWorker() 
        {
            
        }

        internal class RecognitionJobResults 
        {
            public TrenaryResult RecognitionResult { get; set; } = TrenaryResult.Failed;
            public TrenaryResult MatchingResult { get; set; } = TrenaryResult.Failed;
            public TrenaryResult SendingResult { get; set; } = TrenaryResult.Failed;

            public int CombinedStatus => (int)Math.Pow(2, (int)RecognitionResult)
                + ((int)Math.Pow(2, (int)MatchingResult) << 3)
                + ((int)Math.Pow(2, (int)SendingResult) << 6);
            
            public RecognitionJobStatus JobStatus 
            { 
                get => AsRecognitionJobStatus(); 
                set => SetRecognitionJobStatus(value);
            }

            // Complete job status consists of 3 flags each representing trenary result of
            // specific operation: recognize, match and send.
            // Trenary result is converted to 3-bit number: 0=0b001(1), 1=0b010(2), 2=0b100(4).
            // Then it is shifted left according to the order of operation result belongs to.
            private RecognitionJobStatus AsRecognitionJobStatus() =>
                (RecognitionJobStatus)Math.Pow(2, (int)RecognitionResult) 
                | (RecognitionJobStatus)((int)Math.Pow(2, (int)MatchingResult) << 3) 
                | (RecognitionJobStatus)((int)Math.Pow(2, (int)SendingResult) << 6);

            private void SetRecognitionJobStatus(RecognitionJobStatus status) 
            {
                int num = (int)status;
                RecognitionResult = (TrenaryResult)(Convert.ToString(num & 0b111, 2).Length - 1);
                MatchingResult = (TrenaryResult)(Convert.ToString((num >> 3) & 0b111, 2).Length - 1);
                SendingResult = (TrenaryResult)(Convert.ToString((num >> 6) & 0b111, 2).Length - 1);
            }
        }

        public void SetJob(RecognitionJob job)
        {
            if (null == job)
                throw new ArgumentNullException(nameof(job));
            Job = job;
            Status = job.JobStatus;
        }

        public void DismissJob() 
        {
            EnsureJobSet();
            Repository.Remove(Job.Id);
        }

        public void Execute()
        {
            EnsureJobSet();
            JobStarted?.Invoke(this, Job);
            try
            {
                // recognizing ticket if it's not yet recognized or recognized with errors
                if (TrenaryResult.Done != _jobResults.RecognitionResult)
                    Recognize();

                // in case recognition hasn't failed completely, proceed to matching
                if (TrenaryResult.Failed != _jobResults.RecognitionResult) 
                {
                    Match();
                    // send ticket only in case there is no recognition errors and it has exact match
                    if (TrenaryResult.Done == _jobResults.RecognitionResult && TrenaryResult.Done == _jobResults.MatchingResult)
                        Send();
                }
            }
            finally
            {
                JobFinished?.Invoke(this, Job);
            }
        }

        public Task ExecuteAsync() => Task.Run(() => Execute());

        public void Recognize() 
        {
            EnsureJobSet();
            EnsureScanPresent();
            Recognizing?.Invoke(this, Job);
            _jobResults.RecognitionResult = RecognizeCommand?.Execute(Job) ?? TrenaryResult.Failed;
            UpdateStoredJob();
            DoneRecognizing?.Invoke(this, Job);
        }
        public Task RecognizeAsync() => 
            Task.Run(() => 
            Recognize());

        public void Match()
        {
            EnsureJobSet();

            if (AutoupdateWinnersTemplates)
                _ = TryUpdateWinnersTemplate();

            if (SlipDuplicateBreaksFlow()) 
            {
                Status = Job.JobStatus | RecognitionJobStatus.Duplicate;
                Duplicate?.Invoke(this, Job);
                return;
            }

            Matching?.Invoke(this, Job);
            if (TrenaryResult.Done != _jobResults.MatchingResult) 
            {
                _jobResults.MatchingResult = MatchCommand?.Execute(Job) ?? TrenaryResult.Failed;
                UpdateStoredJob();
            }
            DoneMatching?.Invoke(this, Job);
        }

        public void Match(TicketTask ticket) 
        {
            EnsureJobSet();
            Matching?.Invoke(this, Job);
            if (TrenaryResult.Done != _jobResults.MatchingResult)
            {
                Job.MatchStatus = TicketTaskMatcher.Match(Job.RecognizedData, ticket);
                _jobResults.MatchingResult = TicketMatchingError.None == Job.MatchStatus
                    ? TrenaryResult.Done 
                    : Job.MatchStatus <= TicketMatchingError.Price 
                        ? TrenaryResult.Partial 
                        : TrenaryResult.Failed;
                
                if (Job.MatchStatus <= TicketMatchingError.Price)
                    Job.Ticket = ticket;
                
                UpdateStoredJob();
            }
            DoneMatching?.Invoke(this, Job);
        }

        public Task MatchAsync() => Task.Run(() => Match());

        public void Send()
        {
            EnsureJobSet();

            if (CollectTrainingData)
                _ = UpdateTrainData();

            if (IsReadyForSending)
                DismissTicket();

            if (TrenaryResult.Done != _jobResults.SendingResult)
            {
                _jobResults.SendingResult = TrenaryResult.Partial;
                Sending?.Invoke(this, Job);
                _jobResults.SendingResult = SendCommand?.Execute(Job) ?? TrenaryResult.Failed;
                UpdateStoredJob();
                _ = WaitJobSendingComplete().ContinueWith(ar => 
                {
                    DoneSending?.Invoke(this, Job);
                    DropEventHandlers();
                });
            }
        }

        public Task SendAsync() => Task.Run(() => Send());

        public void Check() 
        {
            EnsureJobSet();
            Checking?.Invoke(this, Job);
            _jobResults.SendingResult = CheckCommand?.Execute(Job) ?? TrenaryResult.Failed;
            if (TrenaryResult.Done == _jobResults.SendingResult)
                _jobResults.MatchingResult = TrenaryResult.Done;
            UpdateStoredJob();
            DoneChecking?.Invoke(this, Job);
        }

        public Task CheckAsync() => Task.Run(() => Check());

        public (List<NIWordMatch>, List<NIWordMatch>) ResearchNationalId() 
        {
            EnsureJobSet();
            if (null == Template || Template.IsEmpty)
                throw new InvalidOperationException("Missing required template");
            var block = Template.GetDataBlocks().FirstOrDefault(sb => SlipBlockType.NATID == sb.BlockType);
            if (null == block)
                throw new InvalidOperationException("Template does not contain National Id block");
            using var scan = Job.Scan;
            var rect = Rectangle.Intersect(scan.GetBoundingRect(), block.Bbox);
            using var crop = scan.Clone(rect, scan.PixelFormat);
            var decoder = SlipBlockDecoderFactory.GetDecoder(SlipBlockType.ALTNATID) as NationalIdDecoder;
            return decoder.DecodeInternally(crop);
        }

        public Task<(List<NIWordMatch>, List<NIWordMatch>)> ResearchNationalIdAsync() => Task.Run(() => ResearchNationalId());

        public bool JobMatchesTicket(TicketTask ticket) 
        { 
            var result = TicketTaskMatcher.Match(Job.RecognizedData, ticket);
            return result == TicketMatchingError.None;
        }

        public void Retire()
        {
            if (null == Job) return;
            Retired?.Invoke(this, Job);
            DropEventHandlers();
        }

        public void DismissTicket()
        {
            if (null == Job || null == Job.Ticket) return;
            if (IsReadyForSending || IsCompletedSuccessfully)
                TicketRepository.Remove(Job.Ticket.Id);
        }

        private void DropEventHandlers()
        {
            JobStarted = null;
            JobFinished = null;
            JobStatusChanged = null;
            Recognizing = null;
            DoneRecognizing = null;
            Matching = null;
            DoneMatching = null;
            Sending = null;
            DoneSending = null;
            Retired = null;

        }

        private void EnsureJobSet() 
        {
            if (null == Job)
                throw new InvalidOperationException("Worker has no job. Aborting");
            if (IsRetiring)
                throw new InvalidOperationException("Worker has finished the job and is retiring.");
        }

        private void EnsureScanPresent()
        {
            // in this a bit tricky way we ensure that bitmap achieved via getter is immediately disposed
            if (Job.Scan is Bitmap bmp)
            {
                bmp.Dispose();
                return;
            }
            throw new InvalidOperationException("Worker's job doesn't have an image to recognize. Aborting");
        }

        private void UpdateStoredJob()
        {
            bool statusChanged = Job.JobStatus != _jobResults.JobStatus;
            Job.JobStatus = _jobResults.JobStatus;
            Repository.Update(Job);
            if (statusChanged)
                JobStatusChanged?.Invoke(this, Job);
        }

        private void CompleteJob() 
        {
            _jobResults.JobStatus = RecognitionJobStatus.AllDone;
            UpdateStoredJob();
        }

        private void UpsertScannedSlip()
        {
            if (Job.RecognizedData is { SlipId: var slipId, Topbar: var topbar })
            {
                if (null == slipId)
                    throw new InvalidOperationException($"Trying to add empty slip to sanned storage");
                ScannedSlip scannedSlip = SlipRepository.CreateNew();
                scannedSlip.Id = slipId.GetHashCode();
                scannedSlip.SlipId = slipId;
                scannedSlip.TopBarcode = topbar;
                if (Job.Ticket is { TicketId: var ticketId })
                    scannedSlip.TicketId = ticketId;
                SlipRepository.Upsert(scannedSlip);
            }
        }

        private bool SlipIsProcessed()
        {
            return Job.RecognizedData is { SlipId: var slipId }
                && !string.IsNullOrEmpty(slipId)
                && SlipRepository.HasSlip(slipId);
        }

        private ScannedSlip GetScannedSlip() 
        { 
            return Job.RecognizedData is { SlipId: var slipId } 
                ? SlipRepository.GetSlip(slipId) 
                : null;
        }

        private RecognitionJob FindSlipInJobs() 
        { 
            if (Job.RecognizedData is { SlipId: var slipId })
                return Repository.FindOne(j => 
                    null != j.RecognizedData 
                    && j.RecognizedData.SlipId == slipId 
                    && j.Guid != Job.Guid);
            return null;
        }

        private bool SlipDuplicateBreaksFlow() 
        {
            // check if slip has already been sent to server
            if (SlipIsProcessed() && BreaksOnDuplicate)
            {
                ScannedSlip = GetScannedSlip();
                Job.MatchStatus = TicketMatchingError.Duplicate;
                CompleteJob();
                return true;
            }

            var existingJob = FindSlipInJobs();
            if (null != existingJob) 
            {
                existingJob.ImagePath = Job.ImagePath;
                if (!existingJob.JobStatus.HasFlag(RecognitionJobStatus.Recognized))
                    existingJob.RecognizedData = Job.RecognizedData;
                DismissJob();
                SetJob(Repository.Update(existingJob));
            }
              
            return false;
        }

        private async Task WaitJobSendingComplete() 
        {
            EnsureJobSet();
            do
                await Task.Delay(100);
            while (!Job.JobStatus.HasFlag(RecognitionJobStatus.Sent) && !IsRetiring);
            Status = Job.JobStatus;
            UpsertScannedSlip();
            DismissJob();
            Retire();
        }


        private async Task UpdateTrainData() 
        {
            EnsureScanPresent();
            
            if (null != Template) 
            {
                using var scan = Job.Scan;
                if (!string.IsNullOrEmpty(Job.RecognizedData.UserId))
                    await UpdateTrainingDataOfType(SlipBlockType.NATID, scan, Job.RecognizedData.UserId);
                if (!string.IsNullOrEmpty(Job.RecognizedData.Extra))
                    await UpdateTrainingDataOfType(SlipBlockType.EXTRA, scan, Job.RecognizedData.Extra);
            }
        }

        private async Task UpdateTrainingDataOfType(SlipBlockType type, Bitmap scan, string value) 
        {
            var section = Template.FindSection(sec => sec.HasData && sec.Block.BlockType == type);
            var writer = TrainDataService.GetWriter(type);
            var gdb = new GrayDividingBlender();
            if (null != section)
            {
                using (var img = scan.Clone(section.Block.Bbox, scan.PixelFormat))
                {
                    await writer.AddPage(img, value);
                    await writer.AddPage(gdb.Apply(img), value);
                }
            }
        }

        private async Task TryUpdateWinnersTemplate() 
        {
            if (Job.RecognizedData is SlipData slipData)
            {
                EnsureScanPresent();

                using var scan = Job.Scan;
                var gdb = new GrayDividingBlender();
                var otsu = new OtsuThreshold();
                var blt = new BradleyLocalThresholding();
                gdb.ApplyInPlace(scan);
                blt.ApplyInPlace(scan);
                await WinnersTemplateProvider.InsertTemplate(scan, slipData);
            }
        }
    }
}
