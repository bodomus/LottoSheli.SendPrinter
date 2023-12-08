using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Remote;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands
{
    /// <summary>
    /// Command interface for DI
    /// </summary>
    public interface ICheckCommand : IRecognitionJobCommand
    {
    }

    /// <summary>
    /// Checks Slip ID if it already present on a server
    /// </summary>
    [Command(Basic = typeof(ICheckCommand))]
    public class CheckCommand : RecognitionJobCommand, ICheckCommand
    {
        private IRemoteClient _remoteClient;

        private class CheckResultData
        {
            [JsonProperty("tid")]
            public int TicketId { get; set; }

            [JsonProperty("download_url")]
            public string DownloadUrl { get; set; }
        }

        public CheckCommand(IRemoteClient remoteClient, ILogger<IRecognitionJobCommand> logger)
            : this(Guid.NewGuid().ToString(), remoteClient, logger)
        {
        }

        private CheckCommand(string guid, IRemoteClient remoteClient, ILogger<IRecognitionJobCommand> logger)
        {
            _jobGuid = guid;
            _logger = logger;
            _remoteClient = remoteClient;
        }

        public override IRecognitionJobCommand CreateNew(string guid)
        {
            return new CheckCommand(guid, _remoteClient, _logger);
        }

        protected override TrenaryResult ExecuteInternally(RecognitionJob job)
        {
            if (job.RecognizedData is SlipDataEntity slipData && !string.IsNullOrEmpty(slipData.SlipId)) 
            {
                Task<TrenaryResult> sendTask = _remoteClient.CheckTicketAsync(slipData.SlipId)
                    .ContinueWith(ar => 
                    { 
                        if (ar.IsCompletedSuccessfully && !string.IsNullOrEmpty(ar.Result)) 
                        { 
                            var resultData = JsonConvert.DeserializeObject<CheckResultData>(ar.Result);
                            job.DownloadUrl = resultData.DownloadUrl;
                            if (null == job.Ticket)
                                job.Ticket = new TicketTask { TicketId = resultData.TicketId };
                            return TrenaryResult.Done;
                        }
                        return TrenaryResult.Failed;
                    });
                sendTask.Wait();
                return sendTask.Result;
                
            }
            _logger.LogError("Ticket check failed. Slip ID missing.");
            return TrenaryResult.Failed;
        }
    }
}
