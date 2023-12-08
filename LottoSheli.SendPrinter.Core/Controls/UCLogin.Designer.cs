
namespace LottoSheli.SendPrinter.Core.Controls
{
    partial class UCLogin
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
            this.pnlForm = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.submit_btn = new System.Windows.Forms.Button();
            this.mes_lbl = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlContentMain = new System.Windows.Forms.Panel();
            this.pass_txtbx = new System.Windows.Forms.TextBox();
            this.login_txtbx = new System.Windows.Forms.TextBox();
            this.pnlContentLabel = new System.Windows.Forms.Panel();
            this.pass_lbl = new System.Windows.Forms.Label();
            this.login_lbl = new System.Windows.Forms.Label();
            this.chkRightToLeftDirection = new System.Windows.Forms.CheckBox();
            this.pnlForm.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlContentMain.SuspendLayout();
            this.pnlContentLabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.SystemColors.Control;
            this.pnlForm.Controls.Add(this.btnCancel);
            this.pnlForm.Controls.Add(this.submit_btn);
            this.pnlForm.Controls.Add(this.mes_lbl);
            this.pnlForm.Controls.Add(this.pnlContent);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(0, 0);
            this.pnlForm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(286, 202);
            this.pnlForm.TabIndex = 1;
            this.pnlForm.RightToLeftChanged += new System.EventHandler(this.pnlForm_RightToLeftChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(97, 109);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 26);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // submit_btn
            // 
            this.submit_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.submit_btn.Location = new System.Drawing.Point(192, 109);
            this.submit_btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.submit_btn.Name = "submit_btn";
            this.submit_btn.Size = new System.Drawing.Size(88, 26);
            this.submit_btn.TabIndex = 9;
            this.submit_btn.Text = "Submit";
            this.submit_btn.UseVisualStyleBackColor = true;
            this.submit_btn.Click += new System.EventHandler(this.submit_btn_Click);
            // 
            // mes_lbl
            // 
            this.mes_lbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mes_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mes_lbl.ForeColor = System.Drawing.Color.Black;
            this.mes_lbl.Location = new System.Drawing.Point(0, 156);
            this.mes_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mes_lbl.MaximumSize = new System.Drawing.Size(327, 83);
            this.mes_lbl.Name = "mes_lbl";
            this.mes_lbl.Size = new System.Drawing.Size(286, 46);
            this.mes_lbl.TabIndex = 8;
            this.mes_lbl.Text = " ";
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlContentMain);
            this.pnlContent.Controls.Add(this.pnlContentLabel);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(286, 103);
            this.pnlContent.TabIndex = 10;
            // 
            // pnlContentMain
            // 
            this.pnlContentMain.Controls.Add(this.chkRightToLeftDirection);
            this.pnlContentMain.Controls.Add(this.pass_txtbx);
            this.pnlContentMain.Controls.Add(this.login_txtbx);
            this.pnlContentMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContentMain.Location = new System.Drawing.Point(78, 0);
            this.pnlContentMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlContentMain.Name = "pnlContentMain";
            this.pnlContentMain.Size = new System.Drawing.Size(208, 103);
            this.pnlContentMain.TabIndex = 1;
            // 
            // pass_txtbx
            // 
            this.pass_txtbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pass_txtbx.Location = new System.Drawing.Point(4, 50);
            this.pass_txtbx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pass_txtbx.MaxLength = 16;
            this.pass_txtbx.Name = "pass_txtbx";
            this.pass_txtbx.PasswordChar = '*';
            this.pass_txtbx.Size = new System.Drawing.Size(198, 23);
            this.pass_txtbx.TabIndex = 2;
            // 
            // login_txtbx
            // 
            this.login_txtbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.login_txtbx.Location = new System.Drawing.Point(4, 19);
            this.login_txtbx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_txtbx.MaxLength = 16;
            this.login_txtbx.Name = "login_txtbx";
            this.login_txtbx.Size = new System.Drawing.Size(198, 23);
            this.login_txtbx.TabIndex = 0;
            // 
            // pnlContentLabel
            // 
            this.pnlContentLabel.Controls.Add(this.pass_lbl);
            this.pnlContentLabel.Controls.Add(this.login_lbl);
            this.pnlContentLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContentLabel.Location = new System.Drawing.Point(0, 0);
            this.pnlContentLabel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlContentLabel.Name = "pnlContentLabel";
            this.pnlContentLabel.Size = new System.Drawing.Size(78, 103);
            this.pnlContentLabel.TabIndex = 0;
            // 
            // pass_lbl
            // 
            this.pass_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pass_lbl.Location = new System.Drawing.Point(10, 54);
            this.pass_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pass_lbl.Name = "pass_lbl";
            this.pass_lbl.Size = new System.Drawing.Size(64, 15);
            this.pass_lbl.TabIndex = 3;
            this.pass_lbl.Text = "Password:";
            // 
            // login_lbl
            // 
            this.login_lbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.login_lbl.Location = new System.Drawing.Point(10, 19);
            this.login_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.login_lbl.Name = "login_lbl";
            this.login_lbl.Size = new System.Drawing.Size(64, 15);
            this.login_lbl.TabIndex = 1;
            this.login_lbl.Text = "Login:";
            // 
            // chkRightToLeftDirection
            // 
            this.chkRightToLeftDirection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRightToLeftDirection.Location = new System.Drawing.Point(5, 82);
            this.chkRightToLeftDirection.Margin = new System.Windows.Forms.Padding(2);
            this.chkRightToLeftDirection.Name = "chkRightToLeftDirection";
            this.chkRightToLeftDirection.Size = new System.Drawing.Size(196, 22);
            this.chkRightToLeftDirection.TabIndex = 3;
            this.chkRightToLeftDirection.Text = "Right to Left Direction";
            this.chkRightToLeftDirection.UseVisualStyleBackColor = true;
            this.chkRightToLeftDirection.CheckedChanged += new System.EventHandler(this.chkRightToLeftDirection_CheckedChanged);
            // 
            // UCLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlForm);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UCLogin";
            this.Size = new System.Drawing.Size(286, 202);
            this.RightToLeftChanged += new System.EventHandler(this.UCLogin_RightToLeftChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UCLogin_KeyDown);
            this.pnlForm.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContentMain.ResumeLayout(false);
            this.pnlContentMain.PerformLayout();
            this.pnlContentLabel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Button submit_btn;
        private System.Windows.Forms.Label mes_lbl;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlContentMain;
        private System.Windows.Forms.TextBox pass_txtbx;
        private System.Windows.Forms.TextBox login_txtbx;
        private System.Windows.Forms.Panel pnlContentLabel;
        private System.Windows.Forms.Label pass_lbl;
        private System.Windows.Forms.Label login_lbl;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkRightToLeftDirection;
    }
}
