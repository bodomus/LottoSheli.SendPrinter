using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace LottoSheli.SendPrinter.Core.Controls
{
    public partial class ToasterDialog : Form
    {
        private LogLevel _messagelevel = LogLevel.Information;
        private int _closeTime = 5000;
        private DialogState _dialogState;
        private int _x;
        private int _y;
        private IWin32Window _owner;
        private SystemSound _totasterSound;

        private enum DialogState
        {
            Wait,
            Start,
            Close
        }

        private ToasterDialog(IWin32Window owner, string message, LogLevel messagelevel, int closeTime)
        {
            InitializeComponent();

            lblMessage.Text = message;
            _messagelevel = messagelevel;
            _closeTime = closeTime;
            _owner = owner;

            Init();
        }

        private void Init()
        {
            //check, frown, exclamation-triangle, info-circle

            switch (_messagelevel)
            {
                case LogLevel.Information:
                default:
                    ipbMessageType.IconChar = FontAwesome.Sharp.IconChar.InfoCircle;
                    BackColor = ibtnClose.BackColor = Color.RoyalBlue;
                    _totasterSound = SystemSounds.Asterisk;
                    break;
                case LogLevel.Warning:
                    ipbMessageType.IconChar = FontAwesome.Sharp.IconChar.ExclamationTriangle;
                    BackColor = ibtnClose.BackColor = Color.Orange;
                    _totasterSound = SystemSounds.Exclamation;
                    break;
                case LogLevel.Error:
                    ipbMessageType.IconChar = FontAwesome.Sharp.IconChar.Frown;
                    BackColor = ibtnClose.BackColor = Color.DarkRed;
                    _totasterSound = SystemSounds.Hand;
                    break;
                case LogLevel.Critical:
                    ipbMessageType.IconChar = FontAwesome.Sharp.IconChar.Bolt;
                    BackColor = ibtnClose.BackColor = Color.Red;
                    _totasterSound = SystemSounds.Hand;
                    break;
            }

            Opacity = 0.0;
            StartPosition = FormStartPosition.Manual;

            string fName;
            var screen = _owner != null ? Screen.FromHandle(_owner.Handle) : Screen.PrimaryScreen;

            for (int i = 1; i < 10; i++)
            {
                fName = $"toaster-alert{i}";

                ToasterDialog dlg = (ToasterDialog)Application.OpenForms[fName];
                if (dlg == null)
                {
                    Name = fName;
                    _x = (screen.WorkingArea.X + screen.WorkingArea.Width) - Width + 15;
                    _y = (screen.WorkingArea.Y + screen.WorkingArea.Height) - Height * i;

                    Location = new Point(_x, _y);
                    break;
                }
            }

            _x = (screen.WorkingArea.X + screen.WorkingArea.Width) - Width - 5;

            _totasterSound.Play();
        }

        private void ibtnClose_Click(object sender, EventArgs e)
        {
            tmrClose.Interval = 1;
            _dialogState = DialogState.Close;
        }

        public static void ShowToaster(IWin32Window owner, string message, LogLevel messagelevel = LogLevel.Information, int closeTime = 10000)
        {
            if ((owner is Control parent) && parent?.InvokeRequired == true)
            {
                var d = new Action<IWin32Window, string, LogLevel, int>(ShowToaster);
                parent.BeginInvoke(d, new object[] { owner, message, messagelevel, closeTime });
            }
            else
            {
                var dlg = new ToasterDialog(owner, message, messagelevel, closeTime);
                dlg.Show();
            }
        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            switch (_dialogState)
            {
                case DialogState.Start:
                    tmrClose.Interval = 1;
                    Opacity += 0.1;

                    if (_x < Location.X)
                        Left--;
                    else if (Opacity >= 1.0)
                    {
                        _dialogState = DialogState.Wait;
                    }
                    break;
                case DialogState.Wait:
                    tmrClose.Interval = _closeTime;
                    _dialogState = DialogState.Close;
                    break;
                case DialogState.Close:
                    tmrClose.Interval = 1;
                    Opacity -= 0.1;
                    Left -= 3;
                    if (Opacity <= 0.1)
                    {
                        Close();
                    }
                    break;
                default: throw new NotSupportedException(_dialogState.ToString());
            }
        }

        private void ToasterDialog_Load(object sender, EventArgs e)
        {
            _dialogState = DialogState.Start;
            tmrClose.Interval = 1;
            tmrClose.Start();
        }
    }
}
