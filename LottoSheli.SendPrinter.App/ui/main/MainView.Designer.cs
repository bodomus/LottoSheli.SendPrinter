
namespace LottoSheli.SendPrinter.App
{
    partial class MainView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.ucLeftMenu = new LottoSheli.SendPrinter.App.Controls.UCLeftMenu();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlConnections = new System.Windows.Forms.Panel();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.Controls.Add(this.pnlConnections);
            this.pnlMenu.Controls.Add(this.ucLeftMenu);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.pnlMenu.MaximumSize = new System.Drawing.Size(153, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(153, 601);
            this.pnlMenu.TabIndex = 1;
            // 
            // ucLeftMenu
            // 
            this.ucLeftMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucLeftMenu.Location = new System.Drawing.Point(0, 0);
            this.ucLeftMenu.Margin = new System.Windows.Forms.Padding(1);
            this.ucLeftMenu.Name = "ucLeftMenu";
            this.ucLeftMenu.Size = new System.Drawing.Size(153, 468);
            this.ucLeftMenu.TabIndex = 0;
            this.ucLeftMenu.MenuItemChanged += new System.EventHandler<LottoSheli.SendPrinter.App.EventArg.UCLeftMenuItemChangedEventArgs>(this.ucLeftMenu_MenuItemChanged);
            this.ucLeftMenu.Exit += new System.EventHandler(this.ucLeftMenu_Exit);
            // 
            // pnlContent
            // 
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(153, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1199, 601);
            this.pnlContent.TabIndex = 2;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // pnlConnections
            // 
            this.pnlConnections.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlConnections.Location = new System.Drawing.Point(0, 521);
            this.pnlConnections.Margin = new System.Windows.Forms.Padding(0);
            this.pnlConnections.MinimumSize = new System.Drawing.Size(0, 80);
            this.pnlConnections.Name = "pnlConnections";
            this.pnlConnections.Padding = new System.Windows.Forms.Padding(4);
            this.pnlConnections.Size = new System.Drawing.Size(153, 80);
            this.pnlConnections.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1352, 601);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.MinimumSize = new System.Drawing.Size(1086, 520);
            this.Name = "FormMain";
            this.Text = "Lotto Send Printer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlMenu;
        private Controls.UCLeftMenu ucLeftMenu;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlConnections;
    }
}

