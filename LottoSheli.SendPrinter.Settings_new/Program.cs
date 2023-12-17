using System;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

using NLog;
using NLog.Extensions.Logging;
using LottoSheli.SendPrinter.Settings.Factory;

namespace LottoSheli.SendPrinter.Settings
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static int Main(string[] args)
        {
            try
            {
                MainAsync(args).Wait();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        private static void SetupNLog(IServiceCollection services, IConfiguration config)
        {
            //LogManager.Setup()
            //    .SetupExtensions(s => s.AutoLoadAssemblies(false))
            //    .SetupExtensions(s => s.RegisterConfigSettings(config))
            //    .LoadConfigurationFromSection(config)
            //    .GetCurrentClassLogger();

            //services.AddLogging(builder =>
            //{
            //    builder.AddNLog(config);
            //});
        }

        static async Task MainAsync(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            
            try
            {
                await serviceProvider.GetService<App>().Run();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            //HostBuilderContext hostingContext;
            //IHostEnvironment env = hostingContext.HostingEnvironment;
            var host = Host.CreateDefaultBuilder();
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            //var confFile = $"appsettings.json";
            //var str = $"{env}";
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .Build();
            //serviceCollection.AddLogging();


            //serviceCollection.AddSingleton(LoggerFactory.Create(builder =>
            //{
            //    builder.AddNLog();
            //}));

            //serviceCollection.AddSingleton(configuration);
            //serviceCollection.AddSingleton<DbProviderFactory>(System.Data.SqlClient.SqlClientFactory.Instance);
            serviceCollection.AddTransient<App>();
            //CommonSettings mySetting = configuration.Get<CommonSettings>();

            //ConfigurationManager conf = new ConfigurationManager();
            //var value = conf.Get<CommonSettings>();

            using (var objectsFactory = new AbstarctObjectsFactory(configuration))
            {
                var model = objectsFactory.GetSettings();
            }
        }
    }
}