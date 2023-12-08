using LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Repository;
using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.SlipReader.Decoder;
using LottoSheli.SendPrinter.SlipReader.Template;
using LottoSheli.SendPrinter.SlipReader.Trainer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.ReaderWorkflow
{
    public class RecognitionJobFactory : IRecognitionJobFactory
    {
        private readonly ILogger<RecognitionJobFactory> _logger;
        private readonly IRecognitionJobRepository _jobRepository;
        private readonly ITicketTaskRepository _ticketRepository;
        private readonly IScannedSlipRepository _scannedSlipRepository;
        private readonly ITrainDataService _trainDataService;
        private readonly ISlipBlockDecoderFactory _slipBlockDecoderFactory;
        private readonly IWinnersTemplateProvider _winnersTemplateProvider;

        private readonly IRecognizeCommand _recognizeCommand;
        private readonly IMatchCommand _matchCommand;
        private readonly ISendCommand _sendCommand;
        private readonly ICheckCommand _checkCommand;
        private readonly ISettings _settings;

        public IRecognitionJobRepository Repository => _jobRepository;

        public RecognitionJobFactory(IRecognizeCommand recognizeCommand, IMatchCommand matchCommand, ISendCommand sendCommand, ICheckCommand checkCommand,
            IRecognitionJobRepository jobRepository, ITicketTaskRepository ticketRepository, 
            IScannedSlipRepository scannedSlipRepository, ITrainDataService trainDataService, 
            ISlipBlockDecoderFactory slipBlockDecoderFactory, IWinnersTemplateProvider winnersTemplateProvider,
            ISettings settings, ILogger<RecognitionJobFactory> logger)
        {
            _recognizeCommand = recognizeCommand;
            _matchCommand = matchCommand;
            _sendCommand = sendCommand;
            _checkCommand = checkCommand;
            _jobRepository = jobRepository;
            _ticketRepository = ticketRepository;
            _scannedSlipRepository = scannedSlipRepository;
            _trainDataService = trainDataService;
            _slipBlockDecoderFactory = slipBlockDecoderFactory;
            _winnersTemplateProvider = winnersTemplateProvider;
            _settings = settings;
            _logger = logger;
        }

        public RecognitionJob CreateJob(Bitmap scan, int scanType)
        {
            try
            {
                return _jobRepository.CreateNew(scan, scanType);
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IRecognitionJobWorker CreateJobWorker(Bitmap scan, int scanType)
        {
            return CreateJobWorker(CreateJob(scan, scanType));
        }

        public IRecognitionJobWorker CreateJobWorker(RecognitionJob job)
        {
            var worker = CreateJobWorker(job.Guid);
            worker.SetJob(job);
            return worker;
        }

        public IRecognitionJobWorker CreateJobWorker(string guid)
        {
            var worker = new RecognitionJobWorker
            {
                RecognizeCommand = _recognizeCommand.CreateNew(guid),
                MatchCommand = _matchCommand.CreateNew(guid),
                SendCommand = _sendCommand.CreateNew(guid),
                CheckCommand = _checkCommand.CreateNew(guid),
                Repository = _jobRepository,
                TicketRepository = _ticketRepository,
                SlipRepository = _scannedSlipRepository,
                TrainDataService = _trainDataService,
                SlipBlockDecoderFactory = _slipBlockDecoderFactory,
                WinnersTemplateProvider = _winnersTemplateProvider,
                AutoupdateWinnersTemplates = _settings.AutoupdateWinnersTemplate,
                CollectTrainingData = _settings.CollectTrainingData,
                WinnerTemplateVariants = _settings.WinnerSettings.MaxTemplateVariants
            };
            return worker;
        }

        public IRecognitionJobWorker GetJobWorker(int jobId)
        {
            var job = _jobRepository.GetById(jobId);
            if (null != job) 
                return CreateJobWorker(job);
            return null;
        }

        public IEnumerable<RecognitionJob> GetStoredJobs() => _jobRepository.GetAll();

        public IEnumerable<IRecognitionJobWorker> GetStoredWorkers() 
        { 
            var jobs = _jobRepository.GetAll();
            foreach (var job in jobs)
                yield return CreateJobWorker(job);
        }
    }
}
