using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LottoSheli.SendPrinter.Core.Controls
{
    public partial class UCLogin : UserControl
    {
        /// <summary>
        /// Invokes when Authorization is success.
        /// </summary>
        [Category("Control Actions")]
        [Description("Invokes when Authorization is success.")]
        public event EventHandler Authorized;

        /// <summary>
        /// Invokes when Authorization has rejected.
        /// </summary>
        [Category("Control Actions")]
        [Description("Invokes when Authorization has rejected.")]
        public event EventHandler Rejected;

        /// <summary>
        /// Invokes when credentials should be updated.
        /// </summary>
        [Category("Control Actions")]
        [Description("Invokes when credentials should be updated.")]
        public event EventHandler<AuthorizationEventArgs> UpdateCreatentials;

        /// <summary>
        /// Invokes when credentials should be received.
        /// </summary>
        [Category("Control Actions")]
        [Description("Invokes when credentials should be received.")]
        public event EventHandler<AuthorizationEventArgs> ReceiveCreatentials;

        public UCLogin()
        {
            InitializeComponent();
            pass_txtbx.KeyDown += Pass_txtbx_KeyDown;
            pass_txtbx.Text = "123";
            login_txtbx.Text = "123";
        }

        /// <summary>
        /// Show or hide cancel buton.
        /// </summary>
        [Category("Control Actions")]
        [Description("Show or hide cancel buton.")]
        public bool CancelButtonVisible { get => btnCancel.Visible; set => btnCancel.Visible = value; }

        /// <summary>
        /// Show or hide cancel buton.
        /// </summary>
        [Category("Control Actions")]
        [Description("Show or hide Right to left Direction checkbox.")]
        public bool RightToLeftDirectionCheckboxVisible { get => chkRightToLeftDirection.Visible; set => chkRightToLeftDirection.Visible = value; }

        protected virtual void OnAuthorized(EventArgs e)
        {
            Authorized?.Invoke(this, e);
        }

        protected virtual void OnRejected(EventArgs e)
        {
            Rejected?.Invoke(this, e);
        }

        protected virtual void OnUpdateCreatentials(AuthorizationEventArgs e)
        {
            UpdateCreatentials?.Invoke(this, e);
        }

        protected virtual void OnReceiveCreatentials(AuthorizationEventArgs e)
        {
            ReceiveCreatentials?.Invoke(this, e);
        }

        private void submit_btn_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void UCLogin_RightToLeftChanged(object sender, EventArgs e)
        {
            pnlContentLabel.Dock = RightToLeft == RightToLeft.Yes ? DockStyle.Right : DockStyle.Left;
        }

        private void UCLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DoLogin();

            if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(null, EventArgs.Empty);
            }
        }

        private void Pass_txtbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DoLogin();
        }

        /// <summary>
        /// serves as error/success message after login trial
        /// </summary>
        private void UpdateLabel(string message, Color color)
        {
            mes_lbl.Text = message;
            mes_lbl.ForeColor = color;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnRejected(EventArgs.Empty);
        }

        private void chkRightToLeftDirection_CheckedChanged(object sender, EventArgs e)
        {
            RightToLeft = RightToLeft == RightToLeft.No ? RightToLeft.Yes : RightToLeft.No;
        }

        private void pnlForm_RightToLeftChanged(object sender, EventArgs e)
        {
            pnlContentLabel.Dock = RightToLeft == RightToLeft.Yes ? DockStyle.Right : DockStyle.Left;
        }

        public void ClearData()
        {
            pass_txtbx.Text = string.Empty;
            login_txtbx.Text = string.Empty;
            UpdateLabel(string.Empty, Color.Transparent);
        }

        public void DoLogin() 
        {
            if (InvokeRequired)
                Invoke(DoLogin);
            else 
            {
                string pass = pass_txtbx.Text;
                string login = login_txtbx.Text;

                if (string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(login))
                {
                    UpdateLabel("Password or login can not be empty or contain only white spaces", Color.Firebrick);
                    return;
                }

                var creds = new AuthorizationEventArgs();
                OnReceiveCreatentials(creds);

                if (!string.IsNullOrEmpty(creds.Password) && !string.IsNullOrEmpty(creds.Login))
                {
                    if (SHA256Helper.ComputeHash(pass) == creds.Password && login == creds.Login)
                    {
                        OnAuthorized(EventArgs.Empty);
                        return;
                    }
                    else
                    {
                        UpdateLabel("Either password or login is incorrect", Color.Firebrick);
                        return;
                    }
                }
                else
                {
                    creds = new AuthorizationEventArgs() { Login = login, Password = pass };
                    OnUpdateCreatentials(creds);
                    UpdateLabel("Credentials are successfuly stored. Now press 'enter' or 'submit' to login.", Color.Green);
                }
            }
        }
    }
}
