
namespace LottoSheli.SendPrinter.Core.Controls
{
    partial class ToasterDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ibtnClose = new FontAwesome.Sharp.IconButton();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.ipbMessageType = new FontAwesome.Sharp.IconPictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tmrClose = new System.Windows.Forms.Timer(this.components);
            this.pnlRight.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ipbMessageType)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ibtnClose
            // 
            this.ibtnClose.BackColor = System.Drawing.Color.Transparent;
            this.ibtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibtnClose.FlatAppearance.BorderSize = 0;
            this.ibtnClose.ForeColor = System.Drawing.Color.White;
            this.ibtnClose.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.ibtnClose.IconColor = System.Drawing.Color.White;
            this.ibtnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ibtnClose.IconSize = 40;
            this.ibtnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ibtnClose.Location = new System.Drawing.Point(16, 16);
            this.ibtnClose.Name = "ibtnClose";
            this.ibtnClose.Size = new System.Drawing.Size(39, 40);
            this.ibtnClose.TabIndex = 0;
            this.ibtnClose.UseVisualStyleBackColor = false;
            this.ibtnClose.Click += new System.EventHandler(this.ibtnClose_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(271, 80);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Pay attention! a ticket with sequence 25 has not sent successfully to the server." +
    " Try to send it again manually from Dashboard tab.";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.Transparent;
            this.pnlRight.Controls.Add(this.ibtnClose);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(359, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(71, 80);
            this.pnlRight.TabIndex = 2;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.Controls.Add(this.ipbMessageType);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(88, 80);
            this.pnlLeft.TabIndex = 3;
            // 
            // ipbMessageType
            // 
            this.ipbMessageType.BackColor = System.Drawing.Color.Transparent;
            this.ipbMessageType.IconChar = FontAwesome.Sharp.IconChar.InfoCircle;
            this.ipbMessageType.IconColor = System.Drawing.Color.White;
            this.ipbMessageType.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ipbMessageType.IconSize = 48;
            this.ipbMessageType.Location = new System.Drawing.Point(22, 17);
            this.ipbMessageType.Name = "ipbMessageType";
            this.ipbMessageType.Size = new System.Drawing.Size(48, 48);
            this.ipbMessageType.TabIndex = 0;
            this.ipbMessageType.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(88, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 80);
            this.panel1.TabIndex = 4;
            // 
            // tmrClose
            // 
            this.tmrClose.Interval = 5000;
            this.tmrClose.Tick += new System.EventHandler(this.tmrClose_Tick);
            // 
            // ToasterDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(430, 80);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToasterDialog";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ToasterDialog_Load);
            this.pnlRight.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ipbMessageType)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private FontAwesome.Sharp.IconButton ibtnClose;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconPictureBox ipbMessageType;
        private System.Windows.Forms.Timer tmrClose;
    }
}