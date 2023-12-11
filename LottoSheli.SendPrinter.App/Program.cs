using System.Globalization;
using Microsoft.Extensions.Configuration;
using System.Windows.Forms;
using System.Runtime;
using LottoSheli.SendPrinter.Bootstraper;
using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.Repository;
using LottoSheli.SendPrinter.Core.Monitoring;
using System.Reflection;
using LottoSheli.SendPrinter.App.Presenter;
using LottoSheli.SendPrinter.App.View;

namespace LottoSheli.SendPrinter.App
{
    internal static class Program
    {

        private static readonly Mutex Mutex = new Mutex(true, "{2176DBEC-F1CA-4E38-B17C-9D74FE88B364}");
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            if (Mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    await using (var objectsFactory = new AbstractObjectsFactory(config))
                    {
                        var settingsFactory = objectsFactory.GetSettingsFactory();
                        ISettings settings = settingsFactory.GetSettings();

                        IUserRepository users = objectsFactory.GetUserRepository();
                        object printer = new();
                        //IPrinterDevice printer = objectsFactory.GetPrinterFactory().GetPrinterDevice();
                        ISessionRepository sessionRepository = objectsFactory.GetSessionRepository();

                        Application.SetHighDpiMode(HighDpiMode.SystemAware);
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);

                        LoginView formLogin = new LoginView();
                        LoginPresenter mainPresenter = new LoginPresenter(formLogin, users);

                        Application.Run(formLogin);

                        //var formLogin = new FormLogin(users);
                        var f = formLogin.Presenter.GetResult();
                        var m = f;
                        //Application.Run(formLogin);
                        if (formLogin.DialogResult == DialogResult.OK)
                        {
                            IOcrSettings ocrSettings = settingsFactory.GetOcrSettings();
                            ICommonSettings commonSettings = settingsFactory.GetCommonSettings();
                            //IMonitoringService monService = objectsFactory.GetService<IMonitoringService>();
                            //IPerformanceRecordScheduler perfScheduler = objectsFactory.GetService<IPerformanceRecordScheduler>();
                            //IZabbixPerformanceRecorder zbxRecorder = objectsFactory.GetService<IZabbixPerformanceRecorder>();
                            //zbxRecorder.StationId = settings.StationId;
                            //zbxRecorder.ZabbixUrl = commonSettings.ZabbixUrl;
                            //perfScheduler.AddRecorder(zbxRecorder);

                            //ILoggerPerformanceRecorder logRecorder = objectsFactory.GetService<ILoggerPerformanceRecorder>();
                            //perfScheduler.AddRecorder(logRecorder);
                            //perfScheduler.Initialize(30 * 1000);
                            var formMain = new MainView(
                                formLogin.RightToLeft,
                                formLogin.ScannerMode,
                                objectsFactory.GetLoggerFactory(),
                                objectsFactory.GetLoggerFactory().CreateLoggerUIControl(),
                                settings
                                //,monService,
                                ,sessionRepository
                                ,users
                                //,null
                                //,ocrSettings
                                //,objectsFactory.GetCommandFactory()
                                //,objectsFactory.GetRecognitionJobFactory()
                                //,objectsFactory.GetSendingQueue()
                                //,objectsFactory.GetRecognitionJobQueue()
                                //,objectsFactory.GetSequenceService()
                                //,objectsFactory.GetService<IPrinterQueueService>()
                                );

                            Application.Run(formMain);
                        }
                    }
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }
            }
        }
    }
}