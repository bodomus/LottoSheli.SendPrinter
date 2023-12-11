using System.Globalization;
using Microsoft.Extensions.Configuration;

using NLog;

using LottoSheli.SendPrinter.Bootstraper;
using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.Repository;
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
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    await using (var objectsFactory = new AbstractObjectsFactory(config))
                    {
                        var settingsFactory = objectsFactory.GetSettingsFactory();
                        ISettings settings = settingsFactory.GetSettings();
                        IUserRepository users = objectsFactory.GetUserRepository();
                        ISessionRepository sessionRepository = objectsFactory.GetSessionRepository();
                        LoginView formLogin = (LoginView)objectsFactory.GetFormFactory().CreateLoginForm();

                        Application.Run(formLogin);
                        var loginResult = formLogin.Presenter.GetResult();
                        if (formLogin.DialogResult == DialogResult.OK)
                        {
                            IOcrSettings ocrSettings = settingsFactory.GetOcrSettings();
                            ICommonSettings commonSettings = settingsFactory.GetCommonSettings();
                            var logFactory = objectsFactory.GetLoggerFactory();
                            var conf = LogManager.Configuration;

                            var formMain = new MainView(
                                loginResult.RightToLeft,
                                loginResult.Mode,
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