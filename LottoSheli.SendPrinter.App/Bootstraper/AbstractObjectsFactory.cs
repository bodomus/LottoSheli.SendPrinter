using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Logging;
using Newtonsoft.Json;
using LottoSheli.SendPrinter.Repository;
using LottoSheli.SendPrinter.Repository.LiteDB;
//using LottoSheli.SendPrinter.Settings;

using LottoSheli.SendPrinter.App.ui;
using LottoSheli.SendPrinter.App.ui.login;
using LottoSheli.SendPrinter.App.View;
using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.Settings.RemoteSettings;
using LottoSheli.SendPrinter.Settings.ScannerSettings;
using LottoSheli.SendPrinter.Settings.OcrSettings;

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
            var homeDirectory = config.GetRequiredSection("CommonSettings")["LottoHomeDirectory"];

            Directory.CreateDirectory(homeDirectory);

            var services = new ServiceCollection();
            services.AddSingleton<IAbstractObjectsFactory>(this);
            

            InitLogger(services, config);
            InitSettings(services);
            InitStorage(services);
            InitView(services);
            services.AddSingleton(config);
            _serviceProvider = services.BuildServiceProvider();

            _logger = GetLoggerFactory().CreateLogger<AbstractObjectsFactory>();
        }
        private void InitSettings(ServiceCollection services)
        {
            services.AddSingleton<ISettingsFactory>(new DependencyInjectionSettingsFactory(GetServiceProvider))
                   .AddSingleton<IRemoteSettings, RemoteSettings>()
                   .AddSingleton<IScannerSettings, ScannerSettings>()
                   .AddSingleton<IOcrSettings, OcrSettings>()
                   .AddTransient<ScannerSettingsService>()
                   ;
            //services.AddSingleton<ISettingsFactory>(new DependencyInjectionSettingsFactory(GetServiceProvider))
            //    .AddSingleton(SettingsManager.GetSettings())
            //    .AddSingleton(SettingsManager.GetCommonSettings())
            //    .AddSingleton((svc) => SettingsManager.GetOcrSettings());

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ssK" };
        }

        private void InitView(ServiceCollection services)
        {
            services.AddSingleton<IFormFactory>(new FormFactory(GetServiceProvider))
                .AddTransient<ILoginView, LoginView>();
        }

        private IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
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

        #region IObjectsFactory
        public TService GetService<TService>() => _serviceProvider.GetRequiredService<TService>();

        public IFormFactory GetFormFactory()
        {
            return _serviceProvider.GetRequiredService<IFormFactory>();
        }

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

        public ISettingsFactory GetSettingsFactory()
        {
            return _serviceProvider.GetRequiredService<ISettingsFactory>();
        }
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
