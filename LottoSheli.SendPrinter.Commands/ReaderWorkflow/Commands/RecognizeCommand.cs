using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.SlipReader;
using LottoSheli.SendPrinter.SlipReader.Template;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands
{
    /// <summary>
    /// Command interface for DI
    /// </summary>
    public interface IRecognizeCommand : IRecognitionJobCommand
    {
        SlipTemplate Template { get; set; }
    }
    /// <summary>
    /// Recognizes scanned ticket using SlipReader module
    /// </summary>
    [Command(Basic = typeof(IRecognizeCommand))]
    public class RecognizeCommand : RecognitionJobCommand, IRecognizeCommand
    {
        private readonly ISlipReaderFactory _slipReaderFactory;
       
        public SlipTemplate Template { get; set; }

        public RecognizeCommand(ILogger<IRecognitionJobCommand> logger, ISlipReaderFactory slipReaderFactory)
        {
            _logger = logger;
            _slipReaderFactory = slipReaderFactory;
        }

        protected override TrenaryResult ExecuteInternally(RecognitionJob job)
        {
            
            var reader = _slipReaderFactory.GetReader(job.Guid).SetConcurrentReading(false);

            using (Bitmap img = job.Scan)
            {
                if (null != Template && !Template.IsEmpty)
                    reader.Read(img, Template);
                else
                    reader.Read(img);

                job.RecognizedData = reader.SlipData;
                Template = reader.Template;
                
                return job.RecognizedData.FailedBlocks.Contains(SlipBlockType.NONE)
                    ? TrenaryResult.Failed 
                    : job.RecognizedData.FailedBlocks.Any()
                        ? TrenaryResult.Partial
                        : TrenaryResult.Done;
            }
        }

        public override IRecognitionJobCommand CreateNew(string guid)
        {
            var instance = new RecognizeCommand(_logger, _slipReaderFactory);
            instance._jobGuid = guid;
            return instance;
        }
    }
}
