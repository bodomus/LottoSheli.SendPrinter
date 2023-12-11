using LottoSheli.SendPrinter.Settings;
//using LottoSheli.SendPrinter.App.ChildForms;
using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Commands.OCR;
using LottoSheli.SendPrinter.Commands.Print;
using LottoSheli.SendPrinter.Commands.Tasks;
using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Core.Controls;
using LottoSheli.SendPrinter.DTO;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.OCR;
using LottoSheli.SendPrinter.Scanner;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LottoSheli.SendPrinter.App.Controls.Basic;
//using LottoSheli.SendPrinter.SlipReader.Utils;
using System.ComponentModel;
using LottoSheli.SendPrinter.Commands.ReaderWorkflow;
using LottoSheli.SendPrinter.Scanner.V2;
//using AForge.Imaging.Filters;
//using LottoSheli.SendPrinter.SlipReader;
using LottoSheli.SendPrinter.Entity.Enums;
using System.Diagnostics;
using LottoSheli.SendPrinter.Scanner.Enums;
//using LottoSendPrinter.App;
using LottoSheli.SendPrinter.Remote;
//using Humanizer;

namespace LottoSheli.SendPrinter.App.Controls
{
    public partial class UCScanQueue : UserControl
    {
        public event Action CtrlPButtonsClicked;

        private const string _scanQueueEmptyMessage = "Scan queue is empty.";
        private const int SuccessPocketNumber = 1;
        private const int ErrorPocketNumber = 2;
        private const int MAX_SENDING_BEFORE_WARNING = 6;
        private readonly ILogger<UCScanQueue> _logger;
        private IScanner _scanner;
        private readonly ISettings _settings;
        private readonly IOcrSettings _ocrSettings;
        private readonly IPrintTicketCommand _printTicketCommand;
        private readonly IPrintSummaryCommand _printSummaryCommand;
        private readonly IUpdateTicketBundlesCommand _updateTicketBundlesCommand;
        private readonly ICommandFactory _commandFactory;
        private readonly IRecognitionJobFactory _jobFactory;
        private readonly IRecognitionJobQueue _jobQueue;
        private readonly ISendingQueue _sendingQueue;
        private readonly ISequenceService _sequenceService;

        private readonly Color DangerBackColor = Color.FromArgb(255, 128, 128);
        private readonly Color DangerForeColor = Color.FromArgb(213, 0, 0);
        private readonly Color WarningBackColor = Color.FromArgb(255, 192, 128);
        private readonly Color WarningForeColor = Color.FromArgb(230, 81, 0);
        private readonly Color NormalBackColor = SystemColors.Control;
        private readonly Color MissedTicketBackColor = SystemColors.ActiveCaption;
        private readonly Color ExpectedTicketBackColor = Color.LightGreen;

        private readonly TimeSpan WarningTime = TimeSpan.FromMinutes(40);
        private readonly TimeSpan DangerTime = TimeSpan.FromMinutes(60);

        private SynchronizationContext _syncContext;

        private readonly EntityObservableCollection<TicketTask> _ticketTaskDataSource;

        private bool _breakReprintTask = false;
        private bool _stopOnError = true;
        private bool _stopOnRescan = true;
        private List<TicketTask> _scanSearchTargets = new List<TicketTask>();

        private int _expectedSequence = -1;
        private List<int> _missedSequences = new List<int>();
        private SemaphoreSlim _throttler = new SemaphoreSlim(1);
        

        private UCRecognitionJobList _jobList;
        private UCTicketBundle _ticketBundle;
        private UCScannerState _ucScannerState;
        private UCSendingQueue _ucSendingQueue;
        private UCTicketBundleFilter _ucTicketBundleFilter;
        private UCTicketTaskSummary _ucTicketTaskSummary;
        public UCScanQueue(ILogger<UCScanQueue> logger, IScanner scanner, ISettings settings, IOcrSettings ocrSettings,
            ICommandFactory commandFactory, IRecognitionJobFactory jobFactory, IRecognitionJobQueue jobQueue, 
            ISendingQueue sendingQueue, ISequenceService sequenceService)
        {
            _jobList = new UCRecognitionJobList(jobFactory.Repository);
            _ticketBundle = new UCTicketBundle(commandFactory.CreateCommand<IGetBundledTicketsCommand>(), jobFactory.Repository, scanner);
            _ticketInfo = new UCTicketTableView();
            _logger = logger;
            _syncContext = SynchronizationContext.Current;

            InitializeScanner(scanner);

            _settings = settings;
            _ocrSettings = ocrSettings;
            _throttler = new SemaphoreSlim(1);
            _printTicketCommand = commandFactory.CreateCommand<IPrintTicketCommand>();
            _printSummaryCommand = commandFactory.CreateCommand<IPrintSummaryCommand>();
            _updateTicketBundlesCommand = commandFactory.CreateCommand<IUpdateTicketBundlesCommand>();
            _commandFactory = commandFactory;
            _jobFactory = jobFactory;
            _jobQueue = jobQueue;
            _sendingQueue = sendingQueue;
            _sequenceService = sequenceService;
            _ticketTaskDataSource = commandFactory.ExecuteCommand<ISubscribeEntityStateCommand<TicketTask>, SubscribeEntityStateCommandData<TicketTask>, EntityObservableCollection<TicketTask>>(
                new SubscribeEntityStateCommandData<TicketTask> { FillWithActualState = true, SafeUpdateStrategy = SafeQeueuCollectionUpdate });

            
            InitializeComponent();
            InitializeComponentInternally();
        }

        private void InitializeScanner(IScanner scanner) 
        {
            _scanner = scanner;
            _scanner.ScannerBusy += HandleScannerBusy;
            _scanner.ScannerAvailable += HandleScannerAvailable;
            _scanner.FeederEmpty += HandleEmptyFeeder;

            if (_scanner is IScannerCore core)
            {
                core.ScannerFailsToStart += HandleScannerFailsToStart;
                core.SnippetsReady += HandleScannedAsync;
            }
            else if (_scanner is IScannerController ctrl) 
            {
                ctrl.ScannerStateChanged += HandleScannerState;
                ctrl.SnippetsReady += EnqueueRecognitionJob;
            }
        }

        private void HandleEmptyFeeder(object sender, EventArgs e)
        {
            StopScanning();
        }

        private void InitializeComponentInternally() 
        {
            gdvScanListQueue.AutoGenerateColumns = false;
            gdvScanListQueue.DefaultCellStyle.ForeColor = SystemColors.ControlText;
            gdvScanListQueue.DataBindingComplete += gdvScanListQueue_DataBindingComplete;
            gdvScanListQueue.DataError += gdvScanListQueue_DataError;
            gdvScanListQueue.CellDoubleClick += gdvScanListQueue_CellDoubleClick;

            chkStopOnError.CheckedChanged += (s, e) => { _stopOnError = chkStopOnError.Checked; };
            chkStopOnRescan.CheckedChanged += (s, e) => { _stopOnRescan = chkStopOnRescan.Checked; };

            _jobList.Filter = (job) => job.ScanType == (int)ScanType.BatchScan || job.ScanType == (int)ScanType.SingleScan;

            if (_scanner is IScannerCore core)
            {
                chkStopOnError.Checked = true;
                chkStopOnRescan.Checked = true;
                scLists.Panel2.Controls.Add(_jobList);
                _jobList.Dock = DockStyle.Fill;
                _jobList.SelectionChanged += JobSelectionChanged;
                _jobList.JobDetailsClaimed += HandleJobDetailsClaim;
            }
            else if (_scanner is IScannerController ctrl)
            {
                chkStopOnError.Checked = false;
                chkStopOnRescan.Checked = false;
                scLists.Panel2.Controls.Add(_ticketBundle);
                _ticketBundle.Dock = DockStyle.Fill;
                _ticketBundle.SelectionChanged += JobSelectionChanged;
                _ticketBundle.JobDetailsClaimed += HandleJobDetailsClaim;
            }

            _jobQueue.JobFinished += _jobQueue_JobFinished;

            _tablesScroll.Controls.Add(_ticketInfo);
            _ticketInfo.Location = new Point(0, 0);

            _ucScannerState = new UCScannerState(_scanner, true);
            _ucScannerState.Width = _pnlScannerStatus.Width;
            _pnlScannerStatus.Controls.Add(_ucScannerState);

            _ucSendingQueue = new UCSendingQueue(_sendingQueue);
            _ucSendingQueue.Width = _pnlSendingQueue.Width;
            _pnlSendingQueue.Controls.Add(_ucSendingQueue);

            _ucTicketBundleFilter = new UCTicketBundleFilter(_ticketTaskDataSource, DangerTime);
            _ucTicketBundleFilter.Dock = DockStyle.Top;
            gbScanList.Controls.Add(_ucTicketBundleFilter);
            gbScanList.Controls.SetChildIndex(_ucTicketBundleFilter, 1);

            _ucTicketBundleFilter.FilterChanged += _ucTicketBundleFilter_FilterChanged;

            _ucTicketTaskSummary = new UCTicketTaskSummary(_ticketTaskDataSource);
            _ucTicketTaskSummary.Dock = DockStyle.Top;
            gbScanList.Controls.Add(_ucTicketTaskSummary);
            gbScanList.Controls.SetChildIndex(_ucTicketTaskSummary, 2);

            btnFreeTrack.Click += BtnFreeTrack_Click;
            btnScanSearch.Click += BtnScanSearch_Click;
            
        }

        private void BtnScanSearch_Click(object sender, EventArgs e)
        {
            if (_scanSearchTargets.Count > 0)
                StartScanSearching();
        }

        private void gdvScanListQueue_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e is { ColumnIndex: var colIndex, RowIndex: var rowIndex }) 
            {
                gdvScanListQueue.CancelEdit();
                gdvScanListQueue.CurrentCell = gdvScanListQueue[colIndex, rowIndex];
                gdvScanListQueue.BeginEdit(true);
            }
        }

        private void gdvScanListQueue_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Hiding DataGridView.CurrencyManager error when it attempts to select row index 0 on empty grid.
            // Selection is invoked by the CM itself for no obvious reason and can't be disabled
            _logger.LogDebug($"Error in ticket grid  [R{e.RowIndex}:C{e.ColumnIndex}]: {e.Exception.Message}\n{e.Exception.StackTrace}");
        }

        private void gdvScanListQueue_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (sender is DataGridView dgv)
                dgv.ClearSelection();
        }

        private void gdvScanListQueue_SelectionChanged(object sender, EventArgs e)
        {
            TicketTask ticket = null;
            foreach (var selectedRow in gdvScanListQueue.SelectedRows.Cast<DataGridViewRow>())
                EnsureProperRowSelected(selectedRow.Index);

            if (gdvScanListQueue.SelectedRows.Count > 0)
            {
                if (null != _jobList.SelectedJob)
                    _jobList.ClearSelection();

                var row = gdvScanListQueue.SelectedRows[0];
                ticket = _ticketTaskDataSource[row.Index];
            }
            _ticketInfo.GameType = ticket?.GetGameType() ?? GameType.Unknown;
            _ticketInfo.Tables = ticket?.Tables ?? new List<TicketTable>();
        }

        private void gdvScanListQueue_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_ticketTaskDataSource.Count <= e.RowIndex)
                return;
            var row = gdvScanListQueue.Rows[e.RowIndex];
            var ticket = _ticketTaskDataSource[e.RowIndex];
            var timer = DateTime.Now - ticket.CreatedDate;

            e.CellStyle.BackColor = _scanSearchTargets.Contains(ticket) 
                ? ExpectedTicketBackColor 
                : NormalBackColor;

            if (e.ColumnIndex == 1)
            {
                e.Value = ((Enum)e.Value).GetEnumMemberValue();
            }
            else if (e.ColumnIndex == 4)
            {
                //e.Value = $"{(timer.Days > 0 ? $"{timer.Days}." : string.Empty)}{timer.Hours:d2}:{timer.Minutes:d2}:{timer.Seconds:d2}";
                if (timer > DangerTime)
                {
                    e.CellStyle.ForeColor = DangerForeColor;
                }
                else if (timer > WarningTime)
                {
                    e.CellStyle.ForeColor = WarningForeColor;
                }
                e.Value = timer.Duration().Humanize();
            }
            else if (e.ColumnIndex == 7)
                e.Value = _scanSearchTargets.Contains(ticket);

            if (gdvScanListQueue.Rows[e.RowIndex].Selected)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
            }
        }

        private void gdvScanListQueue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 6)
            {

                if (MessageBox.Show("Do you want to remove specified ticket from the queue? This operation cannot be undone.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    var ticketTask = _ticketTaskDataSource[e.RowIndex];

                    if (_commandFactory.ExecuteCommand<IRemoveTaskCommand, RemoveTaskCommandData, bool>(new RemoveTaskCommandData { Id = ticketTask.Id }))
                        ToasterDialog.ShowToaster(this, "Ticket succesfully removed.");
                }
            }
            if (e.ColumnIndex == 7) 
            {
                var ticketTask = _ticketTaskDataSource[e.RowIndex];
                var targetIndex = _scanSearchTargets.FindIndex(tt => tt.TicketId == ticketTask.TicketId);
                if (targetIndex < 0)
                    _scanSearchTargets.Add(ticketTask);
                else
                    _scanSearchTargets.RemoveAt(targetIndex);
            }
        }

        private void _ucTicketBundleFilter_FilterChanged(object sender, EventArgs e)
        {
            ApplyBundleFilter();
        }

        private void _jobQueue_JobFinished(object sender, RecognitionJobWorker worker)
        {
            if (null != worker.Job.Ticket)
                HighlightMatchedBundle(worker.Job.Ticket);

            if (worker.IsReadyForSending && !worker.IsDuplicate)
                _ = RemoveTicketFromQueue(worker.Job);
            else if (null != worker.Job.Ticket)
                _ucTicketBundleFilter.InformError(worker.Job.Ticket);
            else if (ScanType.Rescan == _scanner.CurrentScanType || worker.IsDuplicate)
                worker.Retire();
        }

        private void HandleJobDetailsClaim(object sender, RecognitionJob job)
        {
            if (null != job)
            {
                var worker = _jobFactory.CreateJobWorker(job);
                ShowStoredResultForm(worker);
            }
        }

        private void JobSelectionChanged(object sender, RecognitionJob job)
        {
            if (null != job && job.RecognizedData is { Tables: var tables, GameType: var gtype})
            {
                gdvScanListQueue.ClearSelection();
                _ticketInfo.GameType = gtype;
                _ticketInfo.Tables = tables;
            }
            else if (0 == gdvScanListQueue.SelectedRows.Count)
            {
                _ticketInfo.GameType = GameType.Unknown;
                _ticketInfo.Tables = new List<TicketTable>();
            }
        }

        #region Scanner Events
        private void HandleScanned(object sender, ScanImageEventArgs e)
        {
            bool isSuccess = false;
            var ms = new MemoryStream(e.Blob);
            var job = _jobFactory.CreateJob(new Bitmap(ms), (int)e.ScanType);
            var worker = _jobFactory.CreateJobWorker(job);
            worker.BreaksOnDuplicate = _stopOnRescan;
            e.ResultedPocketNumber = SuccessPocketNumber;
            try
            {
                isSuccess = RunRecognitionJob(worker);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}\r\n{ex.StackTrace}");
                isSuccess = false; 
            }
            finally 
            {
                e.AbortScan = !isSuccess;
                e.HasError = !isSuccess;
                if (!isSuccess)
                    ShowRecognitionResultForm(worker);
            }
        }

        private void HandleScannedAsync(object sender, ScanImageEventArgs e)
        {
            // checking for scan types available from this page not to interfere with scanning from different pages
            if (e.ScanType == ScanType.TicketSearch || e.ScanType == ScanType.BatchScan || e.ScanType == ScanType.SingleScan)
            Task.Run(async () => 
            {
                var ms = new MemoryStream(e.Blob);
                var job = _jobFactory.CreateJob(new Bitmap(ms), (int)e.ScanType);
                var worker = _jobFactory.CreateJobWorker(job);
                worker.BreaksOnDuplicate = _stopOnRescan;
                
                e.ResultedPocketNumber = SuccessPocketNumber;
                try
                {
                    await _throttler.WaitAsync();
                    await worker.RecognizeAsync();
                    
                    if (worker.IsReadyForMatching) 
                    {
                        // alternative processing if ticket scan search invoked
                        if (e.ScanType == ScanType.TicketSearch && _scanSearchTargets.Count > 0)
                        {
                            bool abortScan = false;
                            foreach (var target in _scanSearchTargets) 
                            {
                                worker.Match(target);
                                abortScan = !worker.Status.HasFlag(RecognitionJobStatus.NotMatched);
                                if (abortScan)
                                    break;
                            }
                            e.AbortScan = abortScan;
                            e.HasError = e.AbortScan;
                            if (e.AbortScan)
                            {
                                StopScanning();
                                ShowRecognitionResultForm(worker);
                            }
                            else
                            {
                                worker.DismissJob();
                                worker.Retire();
                            }
                            return;
                        }

                        await worker.MatchAsync();

                        if (null != worker.Job.Ticket)
                            HighlightMatchedTicket(worker.Job.Ticket);

                        //DEBUG: at rare cases slips are marked as partially recognized although no error. Need collecting data in real use
                        if (worker.Status.HasFlag(RecognitionJobStatus.RecognizedWithErrors)) 
                            InformRecognitionErrors(worker);

                        if (worker.IsReadyForSending && !worker.IsDuplicate) 
                        {
                            e.AbortScan = false;
                            e.HasError = false;
                            _ = worker.SendAsync();
                            return;
                        }
                    }
                    if (null != worker.Job.Ticket)
                        _ucTicketBundleFilter.InformError(worker.Job.Ticket);
                    

                    if (_stopOnError || (worker.IsDuplicate && _stopOnRescan))
                        throw new InvalidOperationException($"Scanning aborted due to error in recognition or illegal rescan");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"SCAN failed with error {ex.Message}\n{ex.StackTrace}");
                    e.AbortScan = true;
                    e.HasError = true;
                    StopScanning();
                    ShowRecognitionResultForm(worker);
                }
                finally 
                {
                    _throttler.Release();
                }
            });
        }

        private void EnqueueRecognitionJob(object sender, ScanImageEventArgs e)
        {
            var ms = new MemoryStream(e.Blob);
            var job = _jobFactory.CreateJob(new Bitmap(ms), (int)e.ScanType);
            var worker = _jobFactory.CreateJobWorker(job) as RecognitionJobWorker;
            _jobQueue.Enqueue(worker);
        }

        private void HandleScannerBusy(object sender, EventArgs e)
        {
            _syncContext.Send(arg =>
            {
                btnScanOne.Enabled = false;
                btnScan.Enabled = false;
                btnRestartScanner.Enabled = false;
            }, null);
        }

        private void HandleScannerAvailable(object sender, EventArgs e)
        {
            _syncContext.Send(arg =>
            {
                btnScanOne.Enabled = true;
                btnScan.Enabled = true;
                btnRestartScanner.Enabled = true;
            }, null);
        }

        private void HandleScannerFailsToStart(object sender, MessageEventArgs e)
        {
            _syncContext.Send(arg =>
            {
                MessageBox.Show(this, e.Message, "Scanner failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }, null);
        }

        private void HandleFeederEmpty(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //await SynchronizeDbContext();
        }

        private void HandleScannerState(object sender, EventArgs e)
        {
            if (_scanner is ScannerController ctrl)
            {
                if (PaniniStates.DeviceOnLine == ctrl.State)
                    HandleScannerAvailable(this, EventArgs.Empty);
                else
                    HandleScannerBusy(this, EventArgs.Empty);
            }
        }
        #endregion Scanner Events

        private bool RunRecognitionJob(IRecognitionJobWorker worker)
        {
            worker.Recognize();
            if (worker.IsReadyForMatching) 
            {
                worker.Match();

                if (null != worker.Job.Ticket)
                    HighlightMatchedTicket(worker.Job.Ticket);

                if (worker.IsReadyForSending && !worker.IsDuplicate) 
                {
                    _ = worker.SendAsync();
                    return true;
                }
            }
            return false;
        }

        private void InformRecognitionErrors(IRecognitionJobWorker worker) 
        {
            _logger.LogWarning("PARTIAL RECOGNITION");
            if (worker.Job.RecognizedData is { ErrorMessage: var errMsg, FailedBlocks: var failedBlocks })
            {
                var blocks = failedBlocks.Select(fb => fb.ToString()).ToArray();
                _logger.LogWarning($"{errMsg ?? "Unknown error"} at\n {string.Join("\n", blocks)}");
            }
        }

        private void ShowRecognitionResultForm(IRecognitionJobWorker worker)
        {
            if (InvokeRequired)
                BeginInvoke(ShowRecognitionResultForm, new object[] { worker });
            else
            {
                worker.BreaksOnDuplicate = false;
                var form = CreateRecognitionResultForm();
                form.SetContent(worker);
                form.ShowDialog(this);
            }
        }

        private void ShowStoredResultForm(IRecognitionJobWorker worker) 
        {
            if (InvokeRequired)
                BeginInvoke(() => ShowStoredResultForm(worker));
            else
            {
                worker.BreaksOnDuplicate = false;
                var form = CreateRecognitionResultForm();
                form.SetPromisedContent(worker);
                form.ShowDialog(this);
            }
        }

        private RecognitionResultForm CreateRecognitionResultForm() 
        {
            var form = new RecognitionResultForm();
            form.FormClosed += OnRecognitionResultFormClose;
            return form;
        }

        private void OnRecognitionResultFormClose(object sender, EventArgs e) 
        {
            if (sender is RecognitionResultForm form)
            {
                if (_scanSearchTargets.Any())
                    _scanSearchTargets.Clear();

                if (null != form.Worker && form.Worker.IsCompletedSuccessfully)
                    form.Worker.DismissJob();

                form.FormClosed -= OnRecognitionResultFormClose;
                form.Dispose();
            }
        }

        private void HandleScanPositionChange(TicketTask ticket) 
        {
            if (chkStopOnError.Checked && _expectedSequence > 0) 
            { 
                var expectedRow = GetRowByTicketSequence(_expectedSequence);
                var actualRow = GetRowByTicketSequence(ticket.Sequence);

                if (null == expectedRow || null == actualRow || actualRow.Index < expectedRow.Index)
                    return;

                while (_expectedSequence < ticket.Sequence)
                    _missedSequences.Add(_expectedSequence++);
            }
            _expectedSequence = ticket.Sequence + 1;
        }

        private void SafeQeueuCollectionUpdate(NotifyCollectionChangedAction action, IEnumerable<TicketTask> affectedEnitites, Action<NotifyCollectionChangedAction, IEnumerable<TicketTask>> invoke)
        {
            if (gdvScanListQueue.InvokeRequired)
            {
                var d = new Action<NotifyCollectionChangedAction, IEnumerable<TicketTask>, Action<NotifyCollectionChangedAction, IEnumerable<TicketTask>>>(SafeQeueuCollectionUpdate);
                gdvScanListQueue.BeginInvoke(d, new object[] { action, affectedEnitites, invoke });
            }
            else
            {
                invoke(action, affectedEnitites);
                SafeScanListGridUpdate();
            }
        }

        private void HighlightMatchedTicket(TicketTask ticket)
        {
            if (null == ticket)
                return;

            if (InvokeRequired)
                BeginInvoke(() => HighlightMatchedTicket(ticket));
            else 
            {
                HighlightMatchedBundle(ticket);

                var row = GetRowByTicketSequence(ticket.Sequence);
                if (null != row && row.Visible)
                {
                    EnsureTicketVisible(ticket);
                    row.Selected = true;
                }

                gdvScanListQueue.Invalidate();
            }
        }

        private void HighlightMatchedBundle(TicketTask ticket) 
        {
            if (InvokeRequired)
                BeginInvoke(() => HighlightMatchedBundle(ticket));
            else
            {
                if (null != ticket && ticket.Bundle != _ucTicketBundleFilter.SelectedBundle && _ucTicketBundleFilter.HasBundle(ticket.Bundle))
                    _ucTicketBundleFilter.SetFilter(ticket.Bundle);
            }
        }

        private void MarkMissedTickets(int skip, int take) 
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => MarkMissedTickets(skip, take));
            }
            else 
            {
                if (0 > take || 0 > skip) return;
                take = Math.Min(gdvScanListQueue.Rows.Count - skip, take);

                foreach (var row in gdvScanListQueue.Rows.Cast<DataGridViewRow>())
                {
                    bool missed = row.Index >= skip && row.Index < skip + take;
                    if (missed && row.DataBoundItem is TicketTask ticket)
                        _missedSequences.Add(ticket.Sequence);
                }
                gdvScanListQueue.Invalidate();
            }
        }

        private void EnsureTicketVisible(TicketTask ticket)
        {
            if (InvokeRequired)
                BeginInvoke(() => EnsureTicketVisible(ticket));
            else 
            {
                var row = GetRowByTicketSequence(ticket.Sequence);
                if (null != row) 
                {
                    try
                    {
                        int firstVisibleIndex = gdvScanListQueue.Rows.GetFirstRow(DataGridViewElementStates.Visible);
                        int lastVisibleIndex = gdvScanListQueue.Rows.GetLastRow(DataGridViewElementStates.Visible);
                        int visibleRowsCount = gdvScanListQueue.Rows.GetRowCount(DataGridViewElementStates.Visible);
                        int expectedTopIndex = Math.Min(Math.Max(firstVisibleIndex, row.Index - (visibleRowsCount / 2)), lastVisibleIndex);
                        while (expectedTopIndex < visibleRowsCount && !gdvScanListQueue.Rows[expectedTopIndex].Visible)
                            expectedTopIndex++;
                        gdvScanListQueue.FirstDisplayedScrollingRowIndex = expectedTopIndex;
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private bool EnsureProperRowSelected(int rowIndex)
        {
            var ticket = _ticketTaskDataSource[rowIndex];
            bool canBeSelected = ticket.Bundle == _ucTicketBundleFilter.SelectedBundle || _ucTicketBundleFilter.SelectedBundle == -1;
            if (!canBeSelected)
                SafeHideRowAtIndex(rowIndex);
            return canBeSelected;
        }

        private Task RemoveTicketFromQueue(RecognitionJob job)
        {
            if (null == job.Ticket) return Task.CompletedTask;

            return Task.Factory.StartNew(async () =>
            {
                var cmdData = new RemoveTaskCommandData { Id = job.Ticket.Id };
                await Task.Delay(300);
                if (!_commandFactory.ExecuteCommand<IRemoveTaskCommand, RemoveTaskCommandData, bool>(cmdData))
                    throw new InvalidOperationException($"Failed to remove matched ticket tid={job.Ticket.Id} from the list");
            });
            
        }

        private void StartScanning(ScanType scanType)
        {
            if (_scanSearchTargets.Any())
                _scanSearchTargets.Clear();

            int currentSendingJobs = _jobList.TotalJobCount;
            if (currentSendingJobs < MAX_SENDING_BEFORE_WARNING
                || DialogResult.OK == MessageBox.Show($"There are {currentSendingJobs} scans being sent. Do you want to scan anyway?", "Sending Queue Overloaded", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)) 
            {
                if (_ticketTaskDataSource?.Count > 0)
                {
                    if (scLists.Panel2.Controls.Contains(_ticketBundle))
                        _ticketBundle.StartBundle();

                    _missedSequences.Clear();
                    _expectedSequence = -1;
                    _scanner.Start(scanType);
                }
                else
                {
                    ToasterDialog.ShowToaster(this, _scanQueueEmptyMessage, LogLevel.Warning);
                }
            }
        }

        private void StopScanning() 
        {
            _scanner.Stop();
        }

        private void StartScanSearching()
        {
            if (_scanSearchTargets.Any())
                _scanner.Start(ScanType.TicketSearch);
            else
            {
                ToasterDialog.ShowToaster(this, "No ticket selected!", LogLevel.Warning);
            }
        }

        #region Scanner Buttons
        private void btnScanOne_Click(object sender, EventArgs e)
        {
            StartScanning(ScanType.SingleScan);
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            StartScanning(ScanType.BatchScan);
        }

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            StopScanning();
        }

        private void btnRestartScanner_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to restart the scanner?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_scanner is IScannerCore core)
                    core.Restart();
                else if (_scanner is IScannerController ctrl)
                    ctrl.ShutDown(true);
            }
        }

        private void BtnFreeTrack_Click(object sender, EventArgs e)
        {
            _scanner.FreeTrack();
        }
        #endregion Scanner Buttons

        private void SafeScanListGridInvalidate()
        {
            if (gdvScanListQueue.InvokeRequired)
            {
                var d = new Action(SafeScanListGridInvalidate);
                gdvScanListQueue.BeginInvoke(d, Array.Empty<object>());
            }
            else
            {
                gdvScanListQueue.Invalidate();

                var listCount = ((IList<TicketTask>)gdvScanListQueue.DataSource).Count;

                gbScanList.Text = listCount > 0 ? $"Scan List ({listCount})" : "Scan List";
            }
        }

        private void SafeScanListGridUpdate()
        {
            if (gdvScanListQueue.InvokeRequired)
            {
                var d = new Action(SafeScanListGridUpdate);
                gdvScanListQueue.BeginInvoke(d, Array.Empty<object>());
            }
            else
            {
                if (gdvScanListQueue.DataSource != null)
                {
                    gdvScanListQueue.Refresh();

                    var listCount = ((IList<TicketTask>)gdvScanListQueue.DataSource).Count;

                    gbScanList.Text = listCount > 0 ? $"Scan List ({listCount})" : "Scan List";
                }
            }
        }

        private void ApplyBundleFilter()
        {
            if (InvokeRequired)
            {
                BeginInvoke(ApplyBundleFilter);
            }
            else
            {
                try
                {
                    gdvScanListQueue.ClearSelection();

                    int bundle = _ucTicketBundleFilter.SelectedBundle;
                    var bundleRows = GetRowsByBundle(bundle).ToList();
                    var rows = gdvScanListQueue.Rows.Cast<DataGridViewRow>().ToList();

                    if (bundleRows.Count > 0)
                        foreach (var row in rows)
                            if (bundleRows.Any(br => row.Index == br.Index))
                                row.Visible = true;
                            else
                                SafeHideRowAtIndex(row.Index);
                }
                finally
                {
                    gdvScanListQueue.Refresh();
                }
            }
        }

        private void SafeHideRowAtIndex(int index)
        {
            if (index > -1 && index < gdvScanListQueue.RowCount)
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[gdvScanListQueue.DataSource];
                cm.SuspendBinding();
                gdvScanListQueue.CurrentCell = null;
                gdvScanListQueue.Rows[index].Selected = false;
                gdvScanListQueue.Rows[index].Visible = false;
                cm.ResumeBinding();
            }
        }

        private void UCScanQueue_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                gridRefreshTimer.Start();
            else
                gridRefreshTimer.Stop();
        }

        private void UCScanQueue_Load(object sender, EventArgs e)
        {
            gdvScanListQueue.DataSource = _ticketTaskDataSource;
            SafeScanListGridUpdate();
            ApplyBundleFilter();
        }

        

        private void gridRefreshTimer_Tick(object sender, EventArgs e)
        {
            //Task.Run(() => {
            //    SafeScanListGridInvalidate();
            //});
        }

        

        private void pnlScanList_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(
                new Pen(
                    new SolidBrush(Color.LightGreen), 8),
                    e.ClipRectangle);
        }

        private void UCScanQueue_RightToLeftChanged(object sender, EventArgs e)
        {
            pnlBatchReprintLabels.Dock = RightToLeft == RightToLeft.Yes ? DockStyle.Right : DockStyle.Left;
            pnlTicketSearch.Dock = RightToLeft == RightToLeft.Yes ? DockStyle.Right : DockStyle.Left;
            pnlGoToIndex.Dock = RightToLeft == RightToLeft.Yes ? DockStyle.Right : DockStyle.Left;
        }

        private void pnlErrorList_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(
                new Pen(
                    new SolidBrush(Color.FromArgb(255, 153, 153)), 8),
                    e.ClipRectangle);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearchTID.Text, out int tid))
            {
                var ticket = _commandFactory.ExecuteCommand<IFindTicketTaskByTicketIdCommand, FindTicketTaskByTicketIdCommandData, TicketTask>
                (new FindTicketTaskByTicketIdCommandData { TicketId = tid });

                if (ticket == null)
                    ToasterDialog.ShowToaster(this, $"Ticket with specified ID = {tid} cannot be found.", LogLevel.Warning);
                else
                    HighlightMatchedTicket(ticket);
            }
        }

        private void btnSequenceSearch_Click(object sender, EventArgs e)
        {
            int seqNum = (int)nudSequenceNum.Value;
            var ticket = _commandFactory.ExecuteCommand<IFindTicketTaskBySequenceCommand, FindTicketTaskBySequenceCommandData, TicketTask>
                (new FindTicketTaskBySequenceCommandData { SeqenceNumber = seqNum });

            if (ticket == null)
                ToasterDialog.ShowToaster(this, "Specified Sequence cannot be found.", LogLevel.Warning);
            else
                HighlightMatchedTicket(ticket);
        }

        private void btnPreviousTicket_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = gdvScanListQueue.SelectedRows[0].Index;
            if (selectedRowIndex > 0)
            {
                gdvScanListQueue.Rows[selectedRowIndex].Selected = false;
                gdvScanListQueue.Rows[--selectedRowIndex].Selected = true;
            }
        }

        private void btnNetxt_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = gdvScanListQueue.SelectedRows[0].Index;
            if (selectedRowIndex < gdvScanListQueue.Rows.Count - 1)
            {
                gdvScanListQueue.Rows[selectedRowIndex].Selected = false;
                gdvScanListQueue.Rows[++selectedRowIndex].Selected = true;
            }
        }

        private async Task<PrintTicketResult> PrintTicket(TicketTask ticketTask)
        {
            var result = await _printTicketCommand.Execute(new PrintTicketCommandData { ExistingTicketTask = ticketTask, Reprinted = true });
            if (result.Error != null)
            {
                ToasterDialog.ShowToaster(this, result.Error.Message, LogLevel.Warning);
            }

            return result;
        }

        private async void btnReprint_Click(object sender, EventArgs e)
        {
            if (gdvScanListQueue.SelectedRows.Count <= 0)
                ToasterDialog.ShowToaster(this, "The \"Scan Queue\" list should be selected.", LogLevel.Warning);
            else
            {
                btnReprint.Enabled = false;
                btnBatchReprint.Enabled = false;
                foreach (DataGridViewRow row in gdvScanListQueue.SelectedRows)
                {
                    var ticketTask = ((IList<TicketTask>)gdvScanListQueue.DataSource)[row.Index];
                    await PrintTicket(ticketTask);
                }
                btnReprint.Enabled = true;
                btnBatchReprint.Enabled = true;
            }
        }

        private async void btnBatchReprint_Click(object sender, EventArgs e)
        {

            if (btnReprint.Text.Equals("Cancel", StringComparison.OrdinalIgnoreCase))
            {
                _breakReprintTask = true;
                return;
            }

            if (nudReprintFrom.Value > nudReprintTo.Value)
                ToasterDialog.ShowToaster(this, "The \"From\" value should not be greather than \"To\" value", LogLevel.Warning);

            btnReprint.Enabled = false;
            btnBatchReprint.Text = "Cancel";

            var filteredTasks = _ticketTaskDataSource.Where(obj => obj.Sequence >= nudReprintFrom.Value && obj.Sequence <= nudReprintTo.Value).ToList();

            foreach ( var ticketTask in filteredTasks)
            {
                await PrintTicket(ticketTask);
                if (_breakReprintTask)
                {
                    _breakReprintTask = false;
                    ToasterDialog.ShowToaster(this, "The operation has been cancelled by user.");
                    break;
                }
            }
            btnReprint.Enabled = true;
            btnBatchReprint.Text = "Batch Re-Print";
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            var reportDialog = new FormSummaryReport(_printSummaryCommand, _updateTicketBundlesCommand);
            reportDialog.ShowDialog();
        }

        

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Visible)
            {
                if (keyData == (Keys.Control | Keys.P))
                {
                    CtrlPButtonsClicked?.Invoke();
                    return true;
                }
                else if (keyData == (Keys.Control | Keys.R))
                {
                    btnScan_Click(btnScan, EventArgs.Empty);
                    return true;
                }
                else if (keyData == (Keys.Control | Keys.S))
                {
                    btnScanOne_Click(btnScan, EventArgs.Empty);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private DataGridViewRow GetRowByTicketSequence(int seq) 
        {
            foreach (var row in gdvScanListQueue.Rows.Cast<DataGridViewRow>())
                if (row.DataBoundItem is TicketTask ticket && ticket.Sequence == seq)
                    return row;
            return null;
        }

        private DataGridViewRow GetRowByTicketId(int ticketId)
        {
            foreach (var row in gdvScanListQueue.Rows.Cast<DataGridViewRow>())
                if (row.DataBoundItem is TicketTask ticket && ticketId == ticket.TicketId)
                    return row;
            return null;
        }

        private IEnumerable<DataGridViewRow> GetRowsByBundle(int bundle)
        {
            int len = gdvScanListQueue.RowCount;
            TicketTask ticket = null;
            DataGridViewRow row = null;
            for (int i = 0; i < len; i++) 
            {
                try
                {
                    ticket = _ticketTaskDataSource[i];
                    row = gdvScanListQueue.Rows[i];
                }
                catch 
                {
                    ticket = null;
                    row = null;
                }
                if (null != ticket && null != row && (-1 == bundle || ticket.Bundle == bundle))
                    yield return row;
            }
        }
    }
}
