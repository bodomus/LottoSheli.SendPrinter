using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Remote;
using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.SlipReader;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands
{
    /// <summary>
    /// Command interface for DI
    /// </summary>
    public interface ISendCommand : IRecognitionJobCommand
    {
    }

    /// <summary>
    /// Sends recognition result and matched ticket using legacy IRemoteClient
    /// </summary>
    [Command(Basic = typeof(ISendCommand))]
    public class SendCommand : RecognitionJobCommand, ISendCommand
    {
        private readonly IRemoteClient _remoteClient;

        public SendCommand(IRemoteClient remoteClient, ILogger<IRecognitionJobCommand> logger) 
            : this(Guid.NewGuid().ToString(), remoteClient, logger)
        { 
        }

        private SendCommand(string guid, IRemoteClient remoteClient, ILogger<IRecognitionJobCommand> logger)
        {
            _jobGuid = guid;
            _remoteClient = remoteClient;
            _logger = logger;
        }

        public override IRecognitionJobCommand CreateNew(string guid)
        {
            return new SendCommand(guid, _remoteClient, _logger);
        }

        protected override TrenaryResult ExecuteInternally(RecognitionJob job)
        {
            var payload = CreatePayload(job);
            var ms = new MemoryStream();
            PrepareScanImage(job.Scan).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            Task<TrenaryResult> sendTask = _remoteClient.SendScanResultAsync(payload, ms)
                .ContinueWith(ar => (ar.IsCompletedSuccessfully && ar.Result) ? TrenaryResult.Done : TrenaryResult.Failed);
            sendTask.Wait();
            return sendTask.Result;
        }

        private Bitmap PrepareScanImage(Bitmap scan) 
        {
            var gdb = new GrayDividingBlender();
            var otsu = new OtsuThreshold();
            var result = otsu.Apply(gdb.Apply(scan));
            return result;
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
                    Tid = job.Ticket.TicketId
                }
            };
        }

        private int[] ParseExtra(string extra) =>
            extra.Split("")
                .Select(x => int.TryParse(x, out int res) ? res : -1)
                .ToArray();
    }
}
