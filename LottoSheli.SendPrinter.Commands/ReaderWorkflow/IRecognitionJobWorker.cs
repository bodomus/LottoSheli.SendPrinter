using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.SlipReader.Decoder;
using LottoSheli.SendPrinter.SlipReader.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow
{
    public interface IRecognitionJobWorker
    {
        RecognitionJob Job { get; }
        RecognitionJobStatus Status { get; set; }
        ScannedSlip ScannedSlip { get; }
        bool IsReadyForMatching { get; }
        bool IsReadyForSending { get; }
        bool IsDuplicate { get; }
        bool IsCompletedSuccessfully { get; }
        bool IsRetiring { get; }
        bool BreaksOnDuplicate { get; set; }
        SlipTemplate Template { get; }

        event EventHandler<RecognitionJob> JobStarted;
        event EventHandler<RecognitionJob> JobFinished;
        event EventHandler<RecognitionJob> JobStatusChanged;
        event EventHandler<RecognitionJob> Recognizing;
        event EventHandler<RecognitionJob> DoneRecognizing;
        event EventHandler<RecognitionJob> Matching;
        event EventHandler<RecognitionJob> DoneMatching;
        event EventHandler<RecognitionJob> Sending;
        event EventHandler<RecognitionJob> DoneSending;
        event EventHandler<RecognitionJob> Checking;
        event EventHandler<RecognitionJob> DoneChecking;
        event EventHandler<RecognitionJob> Retired;
        event EventHandler<RecognitionJob> Duplicate;
        void SetJob(RecognitionJob job);
        void DismissJob();
        void Execute();
        Task ExecuteAsync();
        void Recognize();
        Task RecognizeAsync();
        void Match();
        void Match(TicketTask ticket);
        Task MatchAsync();
        void Send();
        Task SendAsync();
        void Check();
        Task CheckAsync();
        (List<NIWordMatch>, List<NIWordMatch>) ResearchNationalId();
        Task<(List<NIWordMatch>, List<NIWordMatch>)> ResearchNationalIdAsync();
        bool JobMatchesTicket(TicketTask ticket);
        void Retire();
        void DismissTicket();
    }
}
