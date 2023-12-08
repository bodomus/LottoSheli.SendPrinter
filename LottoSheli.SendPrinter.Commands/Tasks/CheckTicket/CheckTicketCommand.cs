using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.OCR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AForge.Imaging.Filters;
using System.Drawing.Imaging;
using System.Drawing;
using LottoSheli.SendPrinter.Commands.OCR;
using LottoSheli.SendPrinter.Remote;
using Newtonsoft.Json;
using System;
using System.IO;
using LottoSheli.SendPrinter.SlipReader;
using System.Linq;

namespace LottoSheli.SendPrinter.Commands.Tasks
{

    /// <summary>
    /// Recognizes ticket for specified <see cref="CheckTicketCommandData"/>
    /// </summary>
    [Command(Basic = typeof(ICheckTicketCommand))]
    public class CheckTicketCommand : ICheckTicketCommand
    {
        private readonly ILogger<CheckTicketCommand> _logger;
        
        private readonly IRemoteClient _remoteClient;
        private readonly ISlipReaderFactory _slipReaderFactory;

        public CheckTicketCommand(ILogger<CheckTicketCommand> logger,
            IRemoteClient remoteClient, ISlipReaderFactory slipReaderFactory)
        {
            _logger = logger;
            _remoteClient = remoteClient;
            _slipReaderFactory = slipReaderFactory;
        }

        private class CheckTicketData
        {
            [JsonProperty("tid")]
            public long TicketId { get; set; }

            [JsonProperty("download_url")]
            public string DownloadUrl { get; set; }
        }


        public bool CanExecute()
        {
            return true;
        }

        public async Task<CheckTicketResult> Execute(CheckTicketCommandData data)
        {

            //Step 1: Receiveng ticket element boxes
            using var cloned = data.Ticket.Clone(data.Ticket.GetImageRectangle(), data.Ticket.PixelFormat);


            var slipReader = _slipReaderFactory.GetReader(string.Empty);

            using (Bitmap img = data.Ticket.Clone(data.Ticket.GetBoundingRect(), data.Ticket.PixelFormat))
            {
                slipReader.Read(img);
                var slipData = slipReader.SlipData;

                if (string.IsNullOrEmpty(slipData.SlipId))
                {
                    var message = "Slip Id is empty.";
                    _logger.LogError(message);

                    return new CheckTicketResult
                    {
                        RecognitionError = new SlipIdNotFoundException(message, null)
                    };
                }

                var json = await _remoteClient.CheckTicketAsync(slipData.SlipId);

                if (string.IsNullOrWhiteSpace(json) || "null".Equals(json, StringComparison.OrdinalIgnoreCase))
                {
                    var message = "Received ticket is empty.";
                    _logger.LogWarning(message);

                    return new CheckTicketResult
                    {
                    };
                }

                var resultTicket = JsonConvert.DeserializeObject<CheckTicketData>(json);

                //Step 4: Returning recognition result
                return new CheckTicketResult
                {
                    ReceivedTicketUrl = resultTicket.DownloadUrl,
                    TicketId = resultTicket.TicketId
                };
            }
        }

    }
}
