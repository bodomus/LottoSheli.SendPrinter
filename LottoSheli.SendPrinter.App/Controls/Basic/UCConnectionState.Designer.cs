namespace LottoSheli.SendPrinter.App.Controls.Basic
{
    partial class UCConnectionState
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
            this._nameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _nameLabel
            // 
            this._nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nameLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this._nameLabel.Location = new System.Drawing.Point(0, 0);
            this._nameLabel.Margin = new System.Windows.Forms.Padding(0);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(80, 24);
            this._nameLabel.TabIndex = 0;
            this._nameLabel.Text = "..";
            this._nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UCConnectionState
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this._nameLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCConnectionState";
            this.Size = new System.Drawing.Size(80, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _nameLabel;
    }
}
