using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

using Microsoft.Extensions.Logging;

using LottoSheli.SendPrinter.App.Controls.Basic;
using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Core.Controls;
using LottoSheli.SendPrinter.Core.Enums;
using LottoSheli.SendPrinter.Core.Monitoring;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Repository;
using LottoSheli.SendPrinter.Settings;
using LottoSheli.SendPrinter.App.Controls.LeftMenu;
using LottoSheli.SendPrinter.Settings.OcrSettings;

namespace LottoSheli.SendPrinter.App
{
    public partial class MainView : Form
    {
        private delegate void SafeShowMessageDelegate(string source, LogLevel logLevel);
        private readonly ILogger<MainView> _logger;
        private readonly IDictionary<LeftMenuItemType, UserControl> _subControls;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ISettingsFactory _settings;
        private readonly IOcrSettings _ocrSettings;
        private readonly IUserRepository _users;
        private ISessionRepository _sessionsRepo;
        private SynchronizationContext _syncContext;
        private ISequenceService _sequenceService;
        private IMonitoringService _monitoringService;

        private UserControl _activeContent;

        public MainView(RightToLeft rightToLeftDirection, 
            ScannerMode scannerMode
            ,ILoggerFactory loggerFactory
            ,Control logControl
            ,ISettingsFactory settings
            ,ISessionRepository sessionsRepo
            ,IUserRepository users
            )
        {
            _loggerFactory = loggerFactory;
            _settings = settings;
            _logger = _loggerFactory.CreateLogger<MainView>();
            _users = users;
            _sessionsRepo = sessionsRepo;
            if (rightToLeftDirection == RightToLeft.Yes)
            {
                RightToLeft = RightToLeft.Yes;
            }
            var s = settings.GetScannerSettings();
            s.Scanner_ImageAdjusments_Brightness = 0;
            s.Scanner_ImageAdjusments_Contrast = 0;
            settings.SaveScannerSettings(s);


            _subControls = new Dictionary<LeftMenuItemType, UserControl>();

            _logger.LogInformation("Started LottoSendPrinter{0}Version info:", Environment.NewLine);

            string[] assemblyNames = new[] { "LottoSheli.SendPrinter.App.exe", "LottoSheli.SendPrinter.Core.dll" };

            foreach (var name in assemblyNames)
            {
                var ver = FileVersionInfo.GetVersionInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), name));
                _logger.LogInformation("    {0} - {1}", name, ver.FileVersion);
            }

            //_logger.LogInformation($"OCR Settings profile name: {_ocrSettings?.OcrSettingsProfileName}");

            InitContent(logControl);

            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
            Text = $"Lotto Send Printer V.{fileVersion.FileVersion}";

            _syncContext = SynchronizationContext.Current;
            _logger.LogInformation("Started LottoSendPrinter{0}Version info:", Environment.NewLine);
            LoadContent();
        }

        private void LoadContent()
        {

            foreach (var contrl in _subControls)
                pnlContent.Controls.Add(contrl.Value);

            InitConnectionStateControl(Role.D7);
            InitConnectionStateControl(Role.D9);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Tab))
            {
                ucLeftMenu.SwitchMenuItem();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InitContent(Control logControl)
        {
            //_sequenceService.Current = _commandFactory.ExecuteCommand<IGetMaxTicketTaskSequenceCommand, int>();
            
            //UCScanQueue scanQueue = new UCScanQueue(
            //    _loggerFactory.CreateLogger<UCScanQueue>(),
            //    _scanner, _settings, _ocrSettings, _commandFactory, _jobFactory, _jobQueue, _sendingQueue, _sequenceService)
            //    { Dock = DockStyle.Fill, Visible = false };

            //UCTicketCheck ticketCheck = new UCTicketCheck(_scanner, _jobFactory, _loggerFactory.CreateLogger<UCTicketCheck>()) 
            //    { Dock = DockStyle.Fill, Visible = false };
            
            //UCPrint print = new UCPrint(
            //    _commandFactory.CreateCommand<IPrintTicketCommand>(),
            //    _commandFactory.CreateCommand<IGetPrintTaskCommand>(),
            //    _commandFactory.CreateCommand<IPrintSummaryCommand>(),
            //    _commandFactory.CreateCommand<IPrintBarcodeCommand>(),
            //    _loggerFactory.CreateLogger<UCPrint>(),
            //    _commandFactory.CreateCommand<ISendPrintResultCommand>(),
            //    _commandFactory.CreateCommand<IVerifyPrinterCommand>(),
            //    _commandFactory.CreateCommand<IUpdateTicketBundlesCommand>(), 
            //    _commandFactory.CreateCommand<IBackupTicketTasksCommand>(),
            //    _sequenceService, 
            //    _printerQueueService, 
            //    _settings)
            //{ Dock = DockStyle.Fill, Visible = false };

            //UCDashboard dashboard = new UCDashboard(_loggerFactory.CreateLogger<UCDashboard>(), _commandFactory,
            //    _jobFactory,
            //    _commandFactory.CreateCommand<IReceiveDrawsCommand>(),
            //    _scannerProcessor.Core,
            //    _ocrSettings)
            //{ Dock = DockStyle.Fill, Visible = false };

            //UCSettingsMain settingsMain = new UCSettingsMain(
            //    _settings,
            //    _commandFactory.CreateCommand<ITestConnectionD9Command>(),
            //    _commandFactory.CreateCommand<ITestConnectionD7Command>(),
            //    _commandFactory.CreateCommand<IResetConnectionCommand>(),
            //    _users,
            //    _sessionsRepo
            //    )
            //{ Dock = DockStyle.Fill, Visible = false };
            //TODO Add controls to the form
            //_subControls.Add(LeftMenuItemType.Dashboard, dashboard);
            //_subControls.Add(LeftMenuItemType.Print, print);
            //_subControls.Add(LeftMenuItemType.ScanQueue, scanQueue);
            //_subControls.Add(LeftMenuItemType.TicketCheck, ticketCheck);
            //_subControls.Add(LeftMenuItemType.SettingsMain, settingsMain);
            //_subControls.Add(LeftMenuItemType.SettingsPrint, new UCSettingsPrint(_printerDevice, _settings) { Dock = DockStyle.Fill, Visible = false });
            //_subControls.Add(LeftMenuItemType.SettingsScan, new UCSettingsScan(_settings) { Dock = DockStyle.Fill, Visible = false });
            //_subControls.Add(LeftMenuItemType.SettingsOCR, new UCSettingsOCR(_ocrSettings, _settings) { Dock = DockStyle.Fill, Visible = false });
            //_subControls.Add(LeftMenuItemType.Logs, new UCLogs() { Dock = DockStyle.Fill, Visible = false, LogControl = logControl });
        }

        private void InitConnectionStateControl(Role serverRole) 
        {
            var stateCtrl = new UCConnectionState
            {
                TargetName = serverRole.ToString(),
                Dock = DockStyle.Bottom
            };

            pnlConnections.Controls.Add(stateCtrl);
        }

        private void UcAuthorize_Rejected(object sender, EventArgs e)
        {
            _subControls[LeftMenuItemType.Authorize].Visible = false;
            SetActiveControl(_activeContent);
        }

        private void UcAuthorize_Authorized(object sender, EventArgs e)
        {
            //_subControls[LeftMenuItemType.Authorize].Visible = false;
            //SetActiveControl((_subControls[LeftMenuItemType.Authorize] as UCAuthorize).SecuredControl);
        }

        private void ShowMessageSafe(string message, LogLevel logLevel)
        {
            if (InvokeRequired)
            {
                var d = new SafeShowMessageDelegate(ShowMessageSafe);
                BeginInvoke(d, new object[] { message, logLevel });
            }
            else
            {
                if (LogLevel.Error == logLevel || LogLevel.Critical == logLevel)
                    MessageBox.Show(message, logLevel.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    ToasterDialog.ShowToaster(this, message, logLevel);
            }
        }

        private void ucLeftMenu_MenuItemChanged(object sender, EventArg.UCLeftMenuItemChangedEventArgs e)
        {
            if (_activeContent is IRevertibleChangeTracking changableContent && changableContent.IsChanged)
            {
                var dialogResult = MessageBox.Show("You have unsaved content. Do you want to save it?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    changableContent.AcceptChanges();
                }
                else if (dialogResult == DialogResult.No)
                {
                    changableContent.RejectChanges();
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }

            foreach (var cntrl in _subControls)
            {
                cntrl.Value.Visible = false;
            }

            if (_subControls.Any())
            {
                if (_subControls.ContainsKey(e.MenuItem))
                {
                    if (_subControls[e.MenuItem].GetType().IsDefined(typeof(RequireAuthenticationAttribute), false))
                    {
                        //(_subControls[LeftMenuItemType.Authorize] as UCAuthorize).SecuredControl = _subControls[e.MenuItem];
                        //(_subControls[LeftMenuItemType.Authorize] as UCAuthorize).ClearData();
                        //_subControls[LeftMenuItemType.Authorize].Visible = true;
                    }
                    else
                    {
                        SetActiveControl(_subControls[e.MenuItem]);
                    }
                }
                else
                    throw new NotImplementedException(nameof(e.MenuItem));
            }
        }

        private void SetActiveControl(UserControl activeContent)
        {
            _activeContent = activeContent;
            _activeContent.Visible = true;
            activeContent.Focus();
        }

        private async void ucLeftMenu_Reconnect(object sender, EventArgs e)
        {
            await Task.Run(async () => 
            {
                var result = MessageBox.Show("This operation will break all active connections. Are you sure you want to reconnect to server?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                _logger.LogInformation("RECONNECTING");

                if (result == DialogResult.Yes)
                {
                    throw new NotImplementedException("Remove ");
                    //_sendingQueue.Stop();
                    //await _commandFactory.ExecuteCommandAsync<IResetConnectionCommand>();
                    //_sendingQueue.Start();
                }
            });
        }

        private void ucLeftMenu_Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show(this, "You must print sort report first or all results will be lost.",
                "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            e.Cancel = res == DialogResult.Cancel;
        }

        private IList<string> ReceiveFiles(bool singleScan)
        {
            var tickets = new List<string>();
            new Thread(() =>
            {
                OpenFileDialog dlg = new OpenFileDialog
                {
                    Filter = "Image files (*.png, *.bmp)|*.png;*.bmp",
                    RestoreDirectory = true,
                    Multiselect = !singleScan
                };
                if (dlg.ShowDialog() == DialogResult.OK && dlg.FileNames.Length > 0)
                {
                    tickets.AddRange(dlg.FileNames);
                }
            }).RunSTA();

            return tickets;
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
