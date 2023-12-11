namespace LottoSheli.SendPrinter.App.View
{
    partial class LoginView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
            this.ucLogin = new LottoSheli.SendPrinter.Core.Controls.UCLogin();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.pnlMode = new System.Windows.Forms.Panel();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.pnlMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucLogin
            // 
            this.ucLogin.CancelButtonVisible = false;
            this.ucLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogin.Location = new System.Drawing.Point(0, 0);
            this.ucLogin.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.ucLogin.Name = "ucLogin";
            this.ucLogin.RightToLeftDirectionCheckboxVisible = true;
            this.ucLogin.Size = new System.Drawing.Size(271, 182);
            this.ucLogin.TabIndex = 0;
            this.ucLogin.Authorized += new System.EventHandler(this.ucLogin_Authorized);
            this.ucLogin.Rejected += new System.EventHandler(this.ucLogin_Rejected);
            this.ucLogin.UpdateCreatentials += new System.EventHandler<LottoSheli.SendPrinter.Core.AuthorizationEventArgs>(this.ucLogin_UpdateCreatentials);
            this.ucLogin.ReceiveCreatentials += new System.EventHandler<LottoSheli.SendPrinter.Core.AuthorizationEventArgs>(this.ucLogin_ReceiveCreatentials);
            this.ucLogin.RightToLeftChanged += new System.EventHandler(this.ucLogin_RightToLeftChanged);
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "Normal Mode",
            "Demo Mode",
            "Controller Mode"});
            this.cbMode.Location = new System.Drawing.Point(2, 7);
            this.cbMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(125, 23);
            this.cbMode.TabIndex = 1;
            // 
            // pnlMode
            // 
            this.pnlMode.Controls.Add(this.cbVersion);
            this.pnlMode.Controls.Add(this.cbMode);
            this.pnlMode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMode.Location = new System.Drawing.Point(0, 182);
            this.pnlMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMode.Name = "pnlMode";
            this.pnlMode.Size = new System.Drawing.Size(271, 32);
            this.pnlMode.TabIndex = 2;
            // 
            // cbVersion
            // 
            this.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Items.AddRange(new object[] {
            "V1",
            "V2"});
            this.cbVersion.Location = new System.Drawing.Point(182, 7);
            this.cbVersion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(87, 23);
            this.cbVersion.TabIndex = 2;
            this.cbVersion.Visible = false;
            this.cbVersion.SelectedIndexChanged += new System.EventHandler(this.cbVersion_SelectedIndexChanged);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 214);
            this.Controls.Add(this.ucLogin);
            this.Controls.Add(this.pnlMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.pnlMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LottoSheli.SendPrinter.Core.Controls.UCLogin ucLogin;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.Panel pnlMode;
        private System.Windows.Forms.ComboBox cbVersion;
    }
}