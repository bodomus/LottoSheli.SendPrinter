using LottoSheli.SendPrinter.App.Presenter;
using LottoSheli.SendPrinter.App.ui.login;
using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Core.Controls;
using LottoSheli.SendPrinter.Core.Enums;
using LottoSheli.SendPrinter.Settings;

namespace LottoSheli.SendPrinter.App.View
{
    /// <summary>
    /// login form
    /// </summary>
    public partial class LoginView : Form, ILoginView
    {
        public event EventHandler<AuthorizationEventArgs> UpdateCredential;
        public event EventHandler<AuthorizationEventArgs> ReceiveCredential;
        private LoginPresenter _presenter;

        public LoginView(IServiceProvider provider)
        {
            InitializeComponent();

            CenterToScreen();
            _presenter = new LoginPresenter(this, provider);

        }

        public LoginPresenter Presenter => _presenter;

        public ScannerMode ScannerMode => (ScannerMode)cbMode.SelectedIndex;

        private void ucLogin_RightToLeftChanged(object sender, EventArgs e)
        {
            RightToLeft = ucLogin.RightToLeft;
        }

        private void ucLogin_Authorized(object sender, EventArgs e)
        {
            _presenter.Autorized();
        }

        private void ucLogin_Rejected(object sender, EventArgs e)
        {
            _presenter.Rejected();
        }

        private void ucLogin_ReceiveCreatentials(object sender, AuthorizationEventArgs e)
        {
            var creds = _presenter.ReceiveCredential();
            e.Login = creds.Login;
            e.Password = creds.Password;
        }

        private void ucLogin_UpdateCreatentials(object sender, AuthorizationEventArgs e)
        {
            Presenter.UpdateCredentials(e.Login, e.Password);
        }

        private void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SettingsManager.OcrSettingsVersion = cbVersion.Text;
        }

        public int ModeSelectedIndex
        {
            get => cbMode.SelectedIndex;
            set => cbMode.SelectedIndex = value;
        }

        public UCLogin UcLogin => this.ucLogin;

        public bool RightToLeftDirectionCheckboxVisible
        {
            get => this.ucLogin.RightToLeftDirectionCheckboxVisible;
            set => this.ucLogin.RightToLeftDirectionCheckboxVisible = value;
        }

        public void CloseDialog()
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        public void RejectDialog()
        {
            Application.Exit();
        }

        UCLogin ILoginView.UcLoginComponent => this.ucLogin;
    }
}
