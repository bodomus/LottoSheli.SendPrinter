
namespace LottoSheli.SendPrinter.App.Controls
{
    partial class UCScanQueue
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this._tablesScroll = new System.Windows.Forms.Panel();
            this.chkStopOnRescan = new System.Windows.Forms.CheckBox();
            this._pnlSendingQueue = new System.Windows.Forms.Panel();
            this.btnSummary = new System.Windows.Forms.Button();
            this.btnFreeTrack = new System.Windows.Forms.Button();
            this._pnlScannerStatus = new System.Windows.Forms.Panel();
            this._scpPanel = new System.Windows.Forms.Panel();
            this.chkStopOnError = new System.Windows.Forms.CheckBox();
            this.btnRestartScanner = new System.Windows.Forms.Button();
            this.gbBatchReprint = new System.Windows.Forms.GroupBox();
            this.pnlBatchReprintControls = new System.Windows.Forms.Panel();
            this.btnBatchReprint = new System.Windows.Forms.Button();
            this.nudReprintTo = new System.Windows.Forms.NumericUpDown();
            this.nudReprintFrom = new System.Windows.Forms.NumericUpDown();
            this.pnlBatchReprintLabels = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStopScan = new System.Windows.Forms.Button();
            this.btnScanOne = new System.Windows.Forms.Button();
            this.btnReprint = new System.Windows.Forms.Button();
            this.btnPreviousTicket = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnNetxt = new System.Windows.Forms.Button();
            this.btnSequenceSearch = new System.Windows.Forms.Button();
            this.nudSequenceNum = new System.Windows.Forms.NumericUpDown();
            this.lblSeqNum = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchTID = new System.Windows.Forms.TextBox();
            this.lblTicketId = new System.Windows.Forms.Label();
            this.scLists = new System.Windows.Forms.SplitContainer();
            this.pnlScanList = new System.Windows.Forms.Panel();
            this.gbScanList = new System.Windows.Forms.GroupBox();
            this.gdvScanListQueue = new System.Windows.Forms.DataGridView();
            this.colSequenceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrintedCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemoveQueue = new System.Windows.Forms.DataGridViewButtonColumn();
            this.emptyColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlTicketSearch = new System.Windows.Forms.Panel();
            this.btnScanSearch = new System.Windows.Forms.Button();
            this.pnlGoToIndex = new System.Windows.Forms.Panel();
            this.gridRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.Panel1.SuspendLayout();
            this.rightSplitContainer.Panel2.SuspendLayout();
            this.rightSplitContainer.SuspendLayout();
            this.gbBatchReprint.SuspendLayout();
            this.pnlBatchReprintControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReprintTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReprintFrom)).BeginInit();
            this.pnlBatchReprintLabels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSequenceNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scLists)).BeginInit();
            this.scLists.Panel1.SuspendLayout();
            this.scLists.SuspendLayout();
            this.pnlScanList.SuspendLayout();
            this.gbScanList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvScanListQueue)).BeginInit();
            this.pnlTicketSearch.SuspendLayout();
            this.pnlGoToIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightSplitContainer
            // 
            this.rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.rightSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rightSplitContainer.Name = "rightSplitContainer";
            this.rightSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightSplitContainer.Panel1
            // 
            this.rightSplitContainer.Panel1.Controls.Add(this._tablesScroll);
            // 
            // rightSplitContainer.Panel2
            // 
            this.rightSplitContainer.Panel2.Controls.Add(this.chkStopOnRescan);
            this.rightSplitContainer.Panel2.Controls.Add(this._pnlSendingQueue);
            this.rightSplitContainer.Panel2.Controls.Add(this.btnSummary);
            this.rightSplitContainer.Panel2.Controls.Add(this.btnFreeTrack);
            this.rightSplitContainer.Panel2.Controls.Add(this._pnlScannerStatus);
            this.rightSplitContainer.Panel2.Controls.Add(this._scpPanel);
            this.rightSplitContainer.Panel2.Controls.Add(this.chkStopOnError);
            this.rightSplitContainer.Panel2.Controls.Add(this.btnRestartScanner);
            this.rightSplitContainer.Panel2.Controls.Add(this.gbBatchReprint);
            this.rightSplitContainer.Panel2.Controls.Add(this.btnStopScan);
            this.rightSplitContainer.Panel2.Controls.Add(this.btnScanOne);
            this.rightSplitContainer.Panel2.Controls.Add(this.btnReprint);
            this.rightSplitContainer.Panel2.Controls.Add(this.btnPreviousTicket);
            this.rightSplitContainer.Panel2.Controls.Add(this.btnScan);
            this.rightSplitContainer.Panel2.Controls.Add(this.btnNetxt);
            this.rightSplitContainer.Size = new System.Drawing.Size(506, 596);
            this.rightSplitContainer.SplitterDistance = 284;
            this.rightSplitContainer.SplitterWidth = 3;
            this.rightSplitContainer.TabIndex = 2;
            // 
            // _tablesScroll
            // 
            this._tablesScroll.AutoScroll = true;
            this._tablesScroll.AutoScrollMargin = new System.Drawing.Size(8, 8);
            this._tablesScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tablesScroll.Location = new System.Drawing.Point(0, 0);
            this._tablesScroll.Margin = new System.Windows.Forms.Padding(0);
            this._tablesScroll.Name = "_tablesScroll";
            this._tablesScroll.Padding = new System.Windows.Forms.Padding(8);
            this._tablesScroll.Size = new System.Drawing.Size(506, 284);
            this._tablesScroll.TabIndex = 0;
            // 
            // chkStopOnRescan
            // 
            this.chkStopOnRescan.AutoSize = true;
            this.chkStopOnRescan.Checked = true;
            this.chkStopOnRescan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStopOnRescan.Location = new System.Drawing.Point(201, 215);
            this.chkStopOnRescan.Name = "chkStopOnRescan";
            this.chkStopOnRescan.Size = new System.Drawing.Size(219, 19);
            this.chkStopOnRescan.TabIndex = 58;
            this.chkStopOnRescan.Text = "Stop if slip has already been scanned";
            this.chkStopOnRescan.UseVisualStyleBackColor = true;
            // 
            // _pnlSendingQueue
            // 
            this._pnlSendingQueue.Location = new System.Drawing.Point(186, 240);
            this._pnlSendingQueue.Name = "_pnlSendingQueue";
            this._pnlSendingQueue.Size = new System.Drawing.Size(320, 48);
            this._pnlSendingQueue.TabIndex = 57;
            // 
            // btnSummary
            // 
            this.btnSummary.Location = new System.Drawing.Point(8, 143);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(177, 34);
            this.btnSummary.TabIndex = 56;
            this.btnSummary.Text = "Print Summary";
            this.btnSummary.UseVisualStyleBackColor = true;
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // btnFreeTrack
            // 
            this.btnFreeTrack.Location = new System.Drawing.Point(400, 103);
            this.btnFreeTrack.Name = "btnFreeTrack";
            this.btnFreeTrack.Size = new System.Drawing.Size(90, 31);
            this.btnFreeTrack.TabIndex = 55;
            this.btnFreeTrack.Text = "Free Track";
            this.btnFreeTrack.UseVisualStyleBackColor = true;
            // 
            // _pnlScannerStatus
            // 
            this._pnlScannerStatus.Location = new System.Drawing.Point(198, 67);
            this._pnlScannerStatus.Name = "_pnlScannerStatus";
            this._pnlScannerStatus.Size = new System.Drawing.Size(292, 24);
            this._pnlScannerStatus.TabIndex = 54;
            // 
            // _scpPanel
            // 
            this._scpPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._scpPanel.Location = new System.Drawing.Point(0, 279);
            this._scpPanel.Margin = new System.Windows.Forms.Padding(0);
            this._scpPanel.Name = "_scpPanel";
            this._scpPanel.Size = new System.Drawing.Size(506, 30);
            this._scpPanel.TabIndex = 52;
            // 
            // chkStopOnError
            // 
            this.chkStopOnError.AutoSize = true;
            this.chkStopOnError.Checked = true;
            this.chkStopOnError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStopOnError.Location = new System.Drawing.Point(201, 193);
            this.chkStopOnError.Name = "chkStopOnError";
            this.chkStopOnError.Size = new System.Drawing.Size(95, 19);
            this.chkStopOnError.TabIndex = 53;
            this.chkStopOnError.Text = "Stop on error";
            this.chkStopOnError.UseVisualStyleBackColor = true;
            // 
            // btnRestartScanner
            // 
            this.btnRestartScanner.Enabled = false;
            this.btnRestartScanner.Location = new System.Drawing.Point(199, 143);
            this.btnRestartScanner.Margin = new System.Windows.Forms.Padding(2);
            this.btnRestartScanner.Name = "btnRestartScanner";
            this.btnRestartScanner.Size = new System.Drawing.Size(144, 34);
            this.btnRestartScanner.TabIndex = 51;
            this.btnRestartScanner.Text = "Restart Scanner";
            this.btnRestartScanner.UseVisualStyleBackColor = true;
            this.btnRestartScanner.Click += new System.EventHandler(this.btnRestartScanner_Click);
            // 
            // gbBatchReprint
            // 
            this.gbBatchReprint.Controls.Add(this.pnlBatchReprintControls);
            this.gbBatchReprint.Controls.Add(this.pnlBatchReprintLabels);
            this.gbBatchReprint.Location = new System.Drawing.Point(4, 2);
            this.gbBatchReprint.Margin = new System.Windows.Forms.Padding(4);
            this.gbBatchReprint.Name = "gbBatchReprint";
            this.gbBatchReprint.Padding = new System.Windows.Forms.Padding(4);
            this.gbBatchReprint.Size = new System.Drawing.Size(185, 82);
            this.gbBatchReprint.TabIndex = 41;
            this.gbBatchReprint.TabStop = false;
            this.gbBatchReprint.Text = "Batch Re-print";
            // 
            // pnlBatchReprintControls
            // 
            this.pnlBatchReprintControls.Controls.Add(this.btnBatchReprint);
            this.pnlBatchReprintControls.Controls.Add(this.nudReprintTo);
            this.pnlBatchReprintControls.Controls.Add(this.nudReprintFrom);
            this.pnlBatchReprintControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBatchReprintControls.Location = new System.Drawing.Point(55, 20);
            this.pnlBatchReprintControls.Margin = new System.Windows.Forms.Padding(2);
            this.pnlBatchReprintControls.Name = "pnlBatchReprintControls";
            this.pnlBatchReprintControls.Size = new System.Drawing.Size(126, 58);
            this.pnlBatchReprintControls.TabIndex = 1;
            // 
            // btnBatchReprint
            // 
            this.btnBatchReprint.Location = new System.Drawing.Point(57, 5);
            this.btnBatchReprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnBatchReprint.Name = "btnBatchReprint";
            this.btnBatchReprint.Size = new System.Drawing.Size(62, 48);
            this.btnBatchReprint.TabIndex = 12;
            this.btnBatchReprint.Text = "Batch Re-Print";
            this.btnBatchReprint.UseVisualStyleBackColor = true;
            this.btnBatchReprint.Click += new System.EventHandler(this.btnBatchReprint_Click);
            // 
            // nudReprintTo
            // 
            this.nudReprintTo.Location = new System.Drawing.Point(4, 34);
            this.nudReprintTo.Margin = new System.Windows.Forms.Padding(2);
            this.nudReprintTo.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudReprintTo.Name = "nudReprintTo";
            this.nudReprintTo.Size = new System.Drawing.Size(49, 23);
            this.nudReprintTo.TabIndex = 1;
            // 
            // nudReprintFrom
            // 
            this.nudReprintFrom.Location = new System.Drawing.Point(4, 5);
            this.nudReprintFrom.Margin = new System.Windows.Forms.Padding(2);
            this.nudReprintFrom.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudReprintFrom.Name = "nudReprintFrom";
            this.nudReprintFrom.Size = new System.Drawing.Size(49, 23);
            this.nudReprintFrom.TabIndex = 0;
            // 
            // pnlBatchReprintLabels
            // 
            this.pnlBatchReprintLabels.Controls.Add(this.lblFrom);
            this.pnlBatchReprintLabels.Controls.Add(this.label2);
            this.pnlBatchReprintLabels.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlBatchReprintLabels.Location = new System.Drawing.Point(4, 20);
            this.pnlBatchReprintLabels.Margin = new System.Windows.Forms.Padding(2);
            this.pnlBatchReprintLabels.Name = "pnlBatchReprintLabels";
            this.pnlBatchReprintLabels.Size = new System.Drawing.Size(51, 58);
            this.pnlBatchReprintLabels.TabIndex = 0;
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(4, 6);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(42, 15);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "From:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "To:";
            // 
            // btnStopScan
            // 
            this.btnStopScan.BackColor = System.Drawing.Color.IndianRed;
            this.btnStopScan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStopScan.Location = new System.Drawing.Point(362, 143);
            this.btnStopScan.Margin = new System.Windows.Forms.Padding(2);
            this.btnStopScan.Name = "btnStopScan";
            this.btnStopScan.Size = new System.Drawing.Size(127, 34);
            this.btnStopScan.TabIndex = 50;
            this.btnStopScan.Text = "Stop Scan";
            this.btnStopScan.UseVisualStyleBackColor = false;
            this.btnStopScan.Click += new System.EventHandler(this.btnStopScan_Click);
            // 
            // btnScanOne
            // 
            this.btnScanOne.Enabled = false;
            this.btnScanOne.Location = new System.Drawing.Point(292, 103);
            this.btnScanOne.Margin = new System.Windows.Forms.Padding(2);
            this.btnScanOne.Name = "btnScanOne";
            this.btnScanOne.Size = new System.Drawing.Size(103, 31);
            this.btnScanOne.TabIndex = 49;
            this.btnScanOne.Text = "Scan";
            this.btnScanOne.UseVisualStyleBackColor = true;
            this.btnScanOne.Click += new System.EventHandler(this.btnScanOne_Click);
            // 
            // btnReprint
            // 
            this.btnReprint.Location = new System.Drawing.Point(8, 104);
            this.btnReprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(177, 31);
            this.btnReprint.TabIndex = 48;
            this.btnReprint.Text = "Re-Print";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // btnPreviousTicket
            // 
            this.btnPreviousTicket.Location = new System.Drawing.Point(201, 21);
            this.btnPreviousTicket.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviousTicket.Name = "btnPreviousTicket";
            this.btnPreviousTicket.Size = new System.Drawing.Size(136, 32);
            this.btnPreviousTicket.TabIndex = 45;
            this.btnPreviousTicket.Text = "<< Previous";
            this.btnPreviousTicket.UseVisualStyleBackColor = true;
            this.btnPreviousTicket.Click += new System.EventHandler(this.btnPreviousTicket_Click);
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.LightGreen;
            this.btnScan.Enabled = false;
            this.btnScan.Location = new System.Drawing.Point(198, 103);
            this.btnScan.Margin = new System.Windows.Forms.Padding(2);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(90, 31);
            this.btnScan.TabIndex = 47;
            this.btnScan.Text = "Run";
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnNetxt
            // 
            this.btnNetxt.Location = new System.Drawing.Point(354, 21);
            this.btnNetxt.Margin = new System.Windows.Forms.Padding(2);
            this.btnNetxt.Name = "btnNetxt";
            this.btnNetxt.Size = new System.Drawing.Size(136, 32);
            this.btnNetxt.TabIndex = 46;
            this.btnNetxt.Text = "Next >>";
            this.btnNetxt.UseVisualStyleBackColor = true;
            this.btnNetxt.Click += new System.EventHandler(this.btnNetxt_Click);
            // 
            // btnSequenceSearch
            // 
            this.btnSequenceSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSequenceSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSequenceSearch.Location = new System.Drawing.Point(151, 0);
            this.btnSequenceSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSequenceSearch.Name = "btnSequenceSearch";
            this.btnSequenceSearch.Size = new System.Drawing.Size(81, 24);
            this.btnSequenceSearch.TabIndex = 1;
            this.btnSequenceSearch.Text = "Go To";
            this.btnSequenceSearch.UseVisualStyleBackColor = true;
            this.btnSequenceSearch.Click += new System.EventHandler(this.btnSequenceSearch_Click);
            // 
            // nudSequenceNum
            // 
            this.nudSequenceNum.Dock = System.Windows.Forms.DockStyle.Right;
            this.nudSequenceNum.Location = new System.Drawing.Point(89, 0);
            this.nudSequenceNum.Margin = new System.Windows.Forms.Padding(2);
            this.nudSequenceNum.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudSequenceNum.Name = "nudSequenceNum";
            this.nudSequenceNum.Size = new System.Drawing.Size(62, 23);
            this.nudSequenceNum.TabIndex = 0;
            // 
            // lblSeqNum
            // 
            this.lblSeqNum.BackColor = System.Drawing.SystemColors.Control;
            this.lblSeqNum.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSeqNum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSeqNum.Location = new System.Drawing.Point(18, 0);
            this.lblSeqNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeqNum.Name = "lblSeqNum";
            this.lblSeqNum.Size = new System.Drawing.Size(71, 24);
            this.lblSeqNum.TabIndex = 0;
            this.lblSeqNum.Text = "Sequnce #:";
            this.lblSeqNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearch.Location = new System.Drawing.Point(156, 8);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnSearch.Size = new System.Drawing.Size(72, 24);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchTID
            // 
            this.txtSearchTID.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtSearchTID.Location = new System.Drawing.Point(70, 8);
            this.txtSearchTID.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearchTID.Name = "txtSearchTID";
            this.txtSearchTID.Size = new System.Drawing.Size(86, 23);
            this.txtSearchTID.TabIndex = 0;
            // 
            // lblTicketId
            // 
            this.lblTicketId.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTicketId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTicketId.Location = new System.Drawing.Point(8, 8);
            this.lblTicketId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTicketId.Name = "lblTicketId";
            this.lblTicketId.Size = new System.Drawing.Size(62, 24);
            this.lblTicketId.TabIndex = 0;
            this.lblTicketId.Text = "Ticket ID:";
            this.lblTicketId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLists
            // 
            this.scLists.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scLists.ForeColor = System.Drawing.SystemColors.Control;
            this.scLists.Location = new System.Drawing.Point(0, 0);
            this.scLists.Margin = new System.Windows.Forms.Padding(2);
            this.scLists.Name = "scLists";
            this.scLists.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scLists.Panel1
            // 
            this.scLists.Panel1.Controls.Add(this.pnlScanList);
            this.scLists.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 9);
            // 
            // scLists.Panel2
            // 
            this.scLists.Panel2.Padding = new System.Windows.Forms.Padding(0, 9, 0, 9);
            this.scLists.Size = new System.Drawing.Size(626, 596);
            this.scLists.SplitterDistance = 400;
            this.scLists.SplitterWidth = 2;
            this.scLists.TabIndex = 0;
            // 
            // pnlScanList
            // 
            this.pnlScanList.Controls.Add(this.gbScanList);
            this.pnlScanList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScanList.Location = new System.Drawing.Point(0, 0);
            this.pnlScanList.Margin = new System.Windows.Forms.Padding(2);
            this.pnlScanList.Name = "pnlScanList";
            this.pnlScanList.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.pnlScanList.Size = new System.Drawing.Size(624, 389);
            this.pnlScanList.TabIndex = 1;
            this.pnlScanList.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlScanList_Paint);
            // 
            // gbScanList
            // 
            this.gbScanList.Controls.Add(this.gdvScanListQueue);
            this.gbScanList.Controls.Add(this.pnlTicketSearch);
            this.gbScanList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbScanList.Location = new System.Drawing.Point(6, 5);
            this.gbScanList.Margin = new System.Windows.Forms.Padding(2);
            this.gbScanList.Name = "gbScanList";
            this.gbScanList.Padding = new System.Windows.Forms.Padding(2);
            this.gbScanList.Size = new System.Drawing.Size(612, 379);
            this.gbScanList.TabIndex = 0;
            this.gbScanList.TabStop = false;
            this.gbScanList.Text = "Scan List";
            // 
            // gdvScanListQueue
            // 
            this.gdvScanListQueue.AllowUserToAddRows = false;
            this.gdvScanListQueue.AllowUserToDeleteRows = false;
            this.gdvScanListQueue.AllowUserToResizeRows = false;
            this.gdvScanListQueue.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.gdvScanListQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvScanListQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSequenceNumber,
            this.colType,
            this.colTID,
            this.colNID,
            this.colTimer,
            this.colPrintedCount,
            this.colRemoveQueue,
            this.emptyColumn});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gdvScanListQueue.DefaultCellStyle = dataGridViewCellStyle7;
            this.gdvScanListQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdvScanListQueue.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gdvScanListQueue.Location = new System.Drawing.Point(2, 58);
            this.gdvScanListQueue.Margin = new System.Windows.Forms.Padding(2);
            this.gdvScanListQueue.MultiSelect = false;
            this.gdvScanListQueue.Name = "gdvScanListQueue";
            this.gdvScanListQueue.RowHeadersVisible = false;
            this.gdvScanListQueue.RowHeadersWidth = 22;
            this.gdvScanListQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gdvScanListQueue.ShowEditingIcon = false;
            this.gdvScanListQueue.ShowRowErrors = false;
            this.gdvScanListQueue.Size = new System.Drawing.Size(608, 319);
            this.gdvScanListQueue.TabIndex = 41;
            this.gdvScanListQueue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gdvScanListQueue_CellContentClick);
            this.gdvScanListQueue.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdvScanListQueue_CellFormatting);
            this.gdvScanListQueue.SelectionChanged += new System.EventHandler(this.gdvScanListQueue_SelectionChanged);
            // 
            // colSequenceNumber
            // 
            this.colSequenceNumber.DataPropertyName = "Sequence";
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.colSequenceNumber.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSequenceNumber.Frozen = true;
            this.colSequenceNumber.HeaderText = "Seq Num.";
            this.colSequenceNumber.MinimumWidth = 8;
            this.colSequenceNumber.Name = "colSequenceNumber";
            this.colSequenceNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSequenceNumber.Width = 48;
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colType.DataPropertyName = "SubType";
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.colType.DefaultCellStyle = dataGridViewCellStyle2;
            this.colType.HeaderText = "Type";
            this.colType.MinimumWidth = 160;
            this.colType.Name = "colType";
            this.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colType.Width = 160;
            // 
            // colTID
            // 
            this.colTID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTID.DataPropertyName = "TicketId";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.colTID.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTID.HeaderText = "TID";
            this.colTID.MinimumWidth = 80;
            this.colTID.Name = "colTID";
            this.colTID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTID.Width = 80;
            // 
            // colNID
            // 
            this.colNID.DataPropertyName = "UserId";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.colNID.DefaultCellStyle = dataGridViewCellStyle4;
            this.colNID.HeaderText = "NID";
            this.colNID.MinimumWidth = 8;
            this.colNID.Name = "colNID";
            this.colNID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNID.Width = 56;
            // 
            // colTimer
            // 
            this.colTimer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTimer.DataPropertyName = "CreatedDate";
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.colTimer.DefaultCellStyle = dataGridViewCellStyle5;
            this.colTimer.HeaderText = "Time";
            this.colTimer.MinimumWidth = 8;
            this.colTimer.Name = "colTimer";
            this.colTimer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // colPrintedCount
            // 
            this.colPrintedCount.DataPropertyName = "PrintedCount";
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.colPrintedCount.DefaultCellStyle = dataGridViewCellStyle6;
            this.colPrintedCount.HeaderText = "Re- Print";
            this.colPrintedCount.MinimumWidth = 8;
            this.colPrintedCount.Name = "colPrintedCount";
            this.colPrintedCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPrintedCount.Width = 48;
            // 
            // colRemoveQueue
            // 
            this.colRemoveQueue.HeaderText = "";
            this.colRemoveQueue.MinimumWidth = 48;
            this.colRemoveQueue.Name = "colRemoveQueue";
            this.colRemoveQueue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colRemoveQueue.Text = "x";
            this.colRemoveQueue.UseColumnTextForButtonValue = true;
            this.colRemoveQueue.Width = 48;
            // 
            // emptyColumn
            // 
            this.emptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.emptyColumn.HeaderText = "";
            this.emptyColumn.MinimumWidth = 48;
            this.emptyColumn.Name = "emptyColumn";
            this.emptyColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.emptyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.emptyColumn.ToolTipText = "Scan search";
            this.emptyColumn.Width = 48;
            // 
            // pnlTicketSearch
            // 
            this.pnlTicketSearch.Controls.Add(this.btnScanSearch);
            this.pnlTicketSearch.Controls.Add(this.pnlGoToIndex);
            this.pnlTicketSearch.Controls.Add(this.btnSearch);
            this.pnlTicketSearch.Controls.Add(this.txtSearchTID);
            this.pnlTicketSearch.Controls.Add(this.lblTicketId);
            this.pnlTicketSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTicketSearch.Location = new System.Drawing.Point(2, 18);
            this.pnlTicketSearch.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTicketSearch.Name = "pnlTicketSearch";
            this.pnlTicketSearch.Padding = new System.Windows.Forms.Padding(8);
            this.pnlTicketSearch.Size = new System.Drawing.Size(608, 40);
            this.pnlTicketSearch.TabIndex = 42;
            // 
            // btnScanSearch
            // 
            this.btnScanSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnScanSearch.Location = new System.Drawing.Point(247, 8);
            this.btnScanSearch.Name = "btnScanSearch";
            this.btnScanSearch.Size = new System.Drawing.Size(103, 23);
            this.btnScanSearch.TabIndex = 4;
            this.btnScanSearch.Text = "Scan Search";
            this.btnScanSearch.UseVisualStyleBackColor = true;
            // 
            // pnlGoToIndex
            // 
            this.pnlGoToIndex.Controls.Add(this.lblSeqNum);
            this.pnlGoToIndex.Controls.Add(this.nudSequenceNum);
            this.pnlGoToIndex.Controls.Add(this.btnSequenceSearch);
            this.pnlGoToIndex.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlGoToIndex.Location = new System.Drawing.Point(368, 8);
            this.pnlGoToIndex.Margin = new System.Windows.Forms.Padding(0);
            this.pnlGoToIndex.Name = "pnlGoToIndex";
            this.pnlGoToIndex.Size = new System.Drawing.Size(232, 24);
            this.pnlGoToIndex.TabIndex = 3;
            // 
            // gridRefreshTimer
            // 
            this.gridRefreshTimer.Interval = 10000;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.scLists);
            this.splitContainer1.Panel1MinSize = 600;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rightSplitContainer);
            this.splitContainer1.Panel2MinSize = 500;
            this.splitContainer1.Size = new System.Drawing.Size(1136, 596);
            this.splitContainer1.SplitterDistance = 626;
            this.splitContainer1.TabIndex = 0;
            // 
            // UCScanQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1129, 525);
            this.Name = "UCScanQueue";
            this.Size = new System.Drawing.Size(1136, 596);
            this.Load += new System.EventHandler(this.UCScanQueue_Load);
            this.RightToLeftChanged += new System.EventHandler(this.UCScanQueue_RightToLeftChanged);
            this.VisibleChanged += new System.EventHandler(this.UCScanQueue_VisibleChanged);
            this.rightSplitContainer.Panel1.ResumeLayout(false);
            this.rightSplitContainer.Panel2.ResumeLayout(false);
            this.rightSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            this.gbBatchReprint.ResumeLayout(false);
            this.pnlBatchReprintControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudReprintTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReprintFrom)).EndInit();
            this.pnlBatchReprintLabels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudSequenceNum)).EndInit();
            this.scLists.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scLists)).EndInit();
            this.scLists.ResumeLayout(false);
            this.pnlScanList.ResumeLayout(false);
            this.gbScanList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdvScanListQueue)).EndInit();
            this.pnlTicketSearch.ResumeLayout(false);
            this.pnlTicketSearch.PerformLayout();
            this.pnlGoToIndex.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer scLists;
        private System.Windows.Forms.GroupBox gbScanList;
        private System.Windows.Forms.DataGridView gdvScanListQueue;
        private System.Windows.Forms.GroupBox gbBatchReprint;
        private System.Windows.Forms.Panel pnlBatchReprintControls;
        private System.Windows.Forms.NumericUpDown nudReprintTo;
        private System.Windows.Forms.NumericUpDown nudReprintFrom;
        private System.Windows.Forms.Panel pnlBatchReprintLabels;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBatchReprint;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearchTID;
        private System.Windows.Forms.Label lblTicketId;
        private System.Windows.Forms.Button btnSequenceSearch;
        private System.Windows.Forms.NumericUpDown nudSequenceNum;
        private System.Windows.Forms.Label lblSeqNum;
        private System.Windows.Forms.Button btnStopScan;
        private System.Windows.Forms.Button btnScanOne;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnNetxt;
        private System.Windows.Forms.Button btnPreviousTicket;
        private System.Windows.Forms.Timer gridRefreshTimer;
        private System.Windows.Forms.Button btnRestartScanner;
        private System.Windows.Forms.SplitContainer rightSplitContainer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlScanList;
        private Basic.UCTicketTableView _ticketInfo;
        private System.Windows.Forms.Panel _tablesScroll;
        private System.Windows.Forms.Panel _scpPanel;
        private System.Windows.Forms.Panel pnlTicketSearch;
        private System.Windows.Forms.Panel pnlGoToIndex;
        private System.Windows.Forms.CheckBox chkStopOnError;
        private System.Windows.Forms.Panel _pnlScannerStatus;
        private System.Windows.Forms.Button btnFreeTrack;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Panel _pnlSendingQueue;
        private System.Windows.Forms.CheckBox chkStopOnRescan;
        private System.Windows.Forms.Button btnScanSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSequenceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrintedCount;
        private System.Windows.Forms.DataGridViewButtonColumn colRemoveQueue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn emptyColumn;
    }
}
