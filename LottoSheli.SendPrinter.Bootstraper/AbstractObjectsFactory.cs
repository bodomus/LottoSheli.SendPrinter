using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using LottoSheli.SendPrinter.Repository;
using LottoSheli.SendPrinter.Repository.LiteDB;
//using LottoSheli.SendPrinter.Core;
//using LottoSheli.SendPrinter.Commands.Base;
//using LottoSheli.SendPrinter.Printer;
//using LottoSheli.SendPrinter.Printer.Renderers;
//using LottoSheli.SendPrinter.Printer.Devices;
using LottoSheli.SendPrinter.Settings;
//using LottoSheli.SendPrinter.Remote;
using LottoSheli.SendPrinter.Repository.Converters;
using LottoSheli.SendPrinter.Repository.LiteDB.Converters;
//using LottoSheli.SendPrinter.SlipReader;
//using LottoSheli.SendPrinter.Commands.ReaderWorkflow;
//using LottoSheli.SendPrinter.Commands.ReaderWorkflow.Commands;
using LottoSheli.SendPrinter.Entity.Enums;
using System.Threading;
//using LottoSheli.SendPrinter.SlipReader.Trainer;
//using LottoSheli.SendPrinter.SlipReader.Decoder;
//using LottoSheli.SendPrinter.OCR;
//using LottoSheli.SendPrinter.OCR.GoogleOCR;
//using LottoSheli.SendPrinter.SlipReader.Template;
//using LottoSheli.SendPrinter.Core.Monitoring;

namespace LottoSheli.SendPrinter.Bootstraper
{

    /// <summary>
    /// Provides Bootstraper objects factory
    /// </summary>
    public sealed class AbstractObjectsFactory : IAbstractObjectsFactory
    {
        private readonly ServiceProvider _serviceProvider;
        private ILogger<AbstractObjectsFactory> _logger;

        public AbstractObjectsFactory(IConfiguration config, bool initOcr = true)
        {
            Directory.CreateDirectory(SettingsManager.LottoHome);

            var services = new ServiceCollection();
            services.AddSingleton<IAbstractObjectsFactory>(this);
            

            InitLogger(services, config);
            InitSettings(services);
            //InitGenerators(services);
            InitStorage(services);
            //InitCommands(services);
            //InitPrinter(services);
            //InitRemoting(services);
            //InitReaderWorkflow(services);
            //InitPerfMonitoring(services);
            //if(initOcr)
            //    InitOcr(services);

            _serviceProvider = services.BuildServiceProvider();

            _logger = GetLoggerFactory().CreateLogger<AbstractObjectsFactory>();
        }

        //private void InitOcr(ServiceCollection services)
        //{
        //    services.AddSingleton<ISlipReaderFactory, SlipReaderFactory>()
        //        .AddSingleton<ISlipBlockDecoderFactory>(SlipBlockDecoderFactory.Instance)
        //        .AddSingleton<IExternalOCRService, GoogleOCRService>()
        //        .AddSingleton<ITrainDataService, TrainDataService>()
        //        .AddSingleton<IWinnersTemplateProvider, WinnersTemplateProvider>();
        //}

        private void InitSettings(ServiceCollection services)
        {
            services.AddSingleton<ISettingsFactory>(new DependencyInjectionSettingsFactory(GetServiceProvider))
                .AddSingleton(SettingsManager.GetSettings())
                .AddSingleton(SettingsManager.GetCommonSettings())
                .AddSingleton((svc) => SettingsManager.GetOcrSettings());

                JsonConvert.DefaultSettings = () => new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ssK" };
        }

        //private void InitPrinter(ServiceCollection services)
        //{
        //    services.AddSingleton<IPrinterFactory>(new DependencyInjectionPrinterFactory(GetServiceProvider))
        //        .AddSingleton<ITicketRenderer, MiphalHaPaisTicketRenderer>()
        //        .AddSingleton<IPrinterQueueService, PrinterQueueService>()
        //        .AddSingleton<IPrinterDevice, PrinterDevice>();
        //}

        private IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }

        private void InitCommands(IServiceCollection services)
        {
            //services.RegisterCommands(GetServiceProvider);
        }

        private static void InitLogger(IServiceCollection services, IConfiguration config)
        {
            services.AddLogging(loggingBuilder => {
                // configure Logging with NLog
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog(config);
            });

            LogManager.Setup().SetupExtensions(ext => ext.RegisterConfigSettings(config));
        }

        //private static void InitGenerators(IServiceCollection services)
        //{
        //    services.AddSingleton<IHashGenerator, SHA1HashGenerator>();
        //    services.AddSingleton<ITicketTaskConverter, TicketTaskConverter>();
        //    services.AddSingleton<IDrawConverter, DrawConverter>();
        //    services.AddSingleton<ISequenceService, SequenceService>();
        //}


        private void InitStorage(IServiceCollection services)
        {
            services.AddSingleton<IRepositoryFactory>(new DependencyInjectionRepositoryFactory(GetServiceProvider))
                .AddSingleton<IDBBackupService, DBBackupService>()
                .AddSingleton<IDBCreatorFactory, DBCreatorFactory>()
                .AddSingleton<ICommonRepository, CommonRepositoryLiteBD>()
                .AddSingleton<ITicketTaskRepository, TicketTaskRepositoryLiteDB>()
                .AddSingleton<IRecognitionJobRepository, RecognitionJobRepositoryLiteDB>()
                .AddSingleton<IScannedSlipRepository, ScannedSlipRepositoryLiteDB>()
                .AddSingleton<IConstantRepository, ConstantRepositoryLiteDB>()
                .AddSingleton<IPrintedTicketLogRepository, PrintedTicketLogRepositoryLiteDB>()
                .AddSingleton<IDrawRepository, DrawRepositoryLiteDB>()
                .AddSingleton<IUserRepository, UserRepositoryLiteDB>()
                .AddSingleton<ISessionRepository, SessionRepositoryLiteDB>()
                .AddSingleton<IRepositoryConfiguration, RepositoryConfigurationLiteDB>();
        }

        //private void InitPerfMonitoring(IServiceCollection services) 
        //{
        //    services.AddSingleton<IMonitoringService, MonitoringService>()
        //        .AddSingleton<IPerformanceRecordScheduler, PerformanceRecordScheduler>()
        //        .AddSingleton<IFilePerformanceRecorder, FilePerformanceRecorder>()
        //        .AddSingleton<ILoggerPerformanceRecorder, LoggerPerformanceRecorder>()
        //        .AddSingleton<IZabbixPerformanceRecorder, ZabbixPerformanceRecorder>();
        //}

        //private void InitRemoting(ServiceCollection services)
        //{
        //    var settings = SettingsManager.GetSettings();
        //    services.AddHttpClient(SendingQueue.HTTP_CLIENT_NAME, GetClientConfigurator(Role.D7))
        //        .ConfigurePrimaryHttpMessageHandler(() => 
        //        {
        //            return new SocketsHttpHandler()
        //            {
        //                PooledConnectionIdleTimeout = TimeSpan.FromSeconds(5),
        //                PooledConnectionLifetime = TimeSpan.FromSeconds(50)
        //            };
        //        });
        //    services.AddHttpClient(Role.D7.ToString(), GetClientConfigurator(Role.D7));
        //    services.AddHttpClient(Role.D9.ToString(), GetClientConfigurator(Role.D9));
        //    services.AddHttpClient($"{Role.D7}{RemoteClient.RESILIENT}", GetClientConfigurator(Role.D7, -1))
        //        .AddTransientHttpErrorPolicy(builder => 
        //            builder.WaitAndRetryForeverAsync(attempt => 
        //                attempt > 10 ? TimeSpan.FromSeconds(10) : TimeSpan.FromSeconds(3)));
        //    services.AddHttpClient($"{Role.D9}{RemoteClient.RESILIENT}", GetClientConfigurator(Role.D9, -1))
        //        .AddTransientHttpErrorPolicy(builder => 
        //            builder.WaitAndRetryForeverAsync(attempt => 
        //                attempt > 10 ? TimeSpan.FromSeconds(10) : TimeSpan.FromSeconds(3)));

        //    services.AddSingleton<IRemoteFactory>(new DependencyInjectionRemoteFactory(GetServiceProvider))
        //        .AddSingleton<IRemoteClient, RemoteClient>()
        //        .AddSingleton<ISendingQueue, SendingQueue>()
        //        .AddSingleton<ISessionManagerFactory, SessionManagerFactory>();
        //}

        //private Action<HttpClient> GetClientConfigurator(Role serverType, int timeoutInSeconds = 120) 
        //{
        //    return (client) => 
        //    {
        //        try
        //        {
        //            var settings = SettingsManager.GetSettings();
        //            var sessionManager = _serviceProvider.GetService<ISessionManagerFactory>().GetSessionManager(serverType);

        //            client.BaseAddress = serverType switch
        //            {
        //                Role.D7 => settings.D7ServerUrl,
        //                Role.D9 => new Uri(settings.D9ServerUrl),
        //                _ => throw new ArgumentException($"Unknown server type: {serverType}")
        //            };
        //            if (sessionManager.HasSession) 
        //            {
        //                foreach (var (key, val) in sessionManager.SessionHeaders)
        //                    if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(val))
        //                        client.DefaultRequestHeaders.Add(key, val);
        //            }
                    
        //            client.Timeout = timeoutInSeconds > 0 
        //                ? TimeSpan.FromSeconds(timeoutInSeconds) 
        //                : Timeout.InfiniteTimeSpan;
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError($"Failed to configure client for {serverType} with error: {ex.Message}\r\n{ex.StackTrace}");
        //            throw;
        //        }
        //    };
        //}

        //private void InitReaderWorkflow(ServiceCollection services) 
        //{
        //    services.AddSingleton<IRecognitionJobFactory, RecognitionJobFactory>()
        //        .AddSingleton<IRecognitionJobQueue, RecognitionJobQueue>()
        //        .AddSingleton<ICheckCommand, CheckCommand>()
        //        //.AddSingleton<ISendCommand, SendCommand>()
        //        .AddSingleton<ISendCommand, SendWithQueueCommand>()
        //        //.AddSingleton<ISendCommand, MockCommand>()
        //        .AddSingleton<IRecognizeCommand, RecognizeCommand>()
        //        //.AddSingleton<IMatchCommand, MockCommand>();
        //        .AddSingleton<IMatchCommand, MatchCommand>();
        //        //.AddSingleton<IMatchCommand, SimMatchCommand>();
        //}

        #region IObjectsFactory
        public TService GetService<TService>() => _serviceProvider.GetRequiredService<TService>();

        public IRepositoryFactory GetRepositoryFactory()
        {
            return _serviceProvider.GetRequiredService<IRepositoryFactory>();
        }

        public IRepositoryConfiguration GetRepositoryConfiguration()
        {
            return _serviceProvider.GetRequiredService<IRepositoryConfiguration>();
        }

        public ILoggerFactory GetLoggerFactory()
        {
            return _serviceProvider.GetService<ILoggerFactory>();
        }

        public IUserRepository GetUserRepository()
        {
            return _serviceProvider.GetService<IUserRepository>();
        }

        public ISessionRepository GetSessionRepository()
        {
            return _serviceProvider.GetService<ISessionRepository>();
        }

        //public ICommandFactory GetCommandFactory()
        //{
        //    return _serviceProvider.GetRequiredService<ICommandFactory>();
        //}

        //public IPrinterFactory GetPrinterFactory()
        //{
        //    return _serviceProvider.GetRequiredService<IPrinterFactory>();
        //}

        public ISettingsFactory GetSettingsFactory()
        {
            return _serviceProvider.GetRequiredService<ISettingsFactory>();
        }

        //public IRemoteFactory GetRemoteFactory()
        //{
        //    return _serviceProvider.GetRequiredService<IRemoteFactory>();
        //}

        //public ISlipReaderFactory GetSlipReaderFactory()
        //{
        //    return _serviceProvider.GetRequiredService<ISlipReaderFactory>();
        //}

        //public IRecognitionJobFactory GetRecognitionJobFactory() 
        //{
        //    return _serviceProvider.GetRequiredService<IRecognitionJobFactory>();
        //}

        //public IRecognitionJobQueue GetRecognitionJobQueue()
        //{
        //    var jobQueue = _serviceProvider.GetRequiredService<IRecognitionJobQueue>();
        //    if (null == jobQueue)
        //        throw new Exception($"Null instead of JobQueue");
        //    return _serviceProvider.GetRequiredService<IRecognitionJobQueue>();
        //}

        //public ISendingQueue GetSendingQueue() 
        //{
        //    return _serviceProvider.GetRequiredService<ISendingQueue>();
        //}

        //public ISequenceService GetSequenceService()
        //{
        //    return _serviceProvider.GetRequiredService<ISequenceService>();
        //}

        //public ISessionManagerFactory GetSessionManagerFactory() 
        //{
        //    return _serviceProvider.GetRequiredService<ISessionManagerFactory>();
        //}
        #endregion IObjectsFactory

        #region IDisposable
        private void Dispose(bool disposing)
        {

            if (disposing)
            {
                (_serviceProvider.GetRequiredService<IDBCreatorFactory>() as IDisposable)?.Dispose();
                (_serviceProvider as IDisposable)?.Dispose();

            }

            //_serviceProvider = null;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable

        #region IAsyncDisposable
        private async ValueTask DisposeAsyncCore()
        {
            if (_serviceProvider is not null)
            {
                _serviceProvider.GetRequiredService<IDBCreatorFactory>()?.Dispose();
                await _serviceProvider.DisposeAsync().ConfigureAwait(false);
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();

            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        
        #endregion
    }
}
