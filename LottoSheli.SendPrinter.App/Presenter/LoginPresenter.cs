using LottoSheli.SendPrinter.Repository;
using System.Windows.Forms;
using LottoSheli.SendPrinter.Core.Enums;
using LottoSheli.SendPrinter.App.ui.login;


namespace LottoSheli.SendPrinter.App.Presenter
{
    public class LoginPresenter
    {
        private ILoginView view;

        private IUserRepository _usersRepository;
        public LoginPresenter(ILoginView loginView, IUserRepository usersRepository)
        {
            view = loginView;
            _usersRepository = usersRepository;
            loginView.Presenter = this;
            view.ModeSelectedIndex = 1;
            view.UcLoginComponent.RightToLeftDirectionCheckboxVisible = false;
        }

        public (RightToLeft rtol, ScannerMode mode) GetResult()
        {
            return (view.UcLoginComponent.RightToLeft, view.ScannerMode);
        }

        public void Autorized()
        {
            view.CloseDialog();
        }

        public void Rejected()
        {
            view.RejectDialog();
        }

        public (string Login, string Password) ReceiveCredential()
        {
            return _usersRepository.GetCredentials();
        }

        public void UpdateCredentials(string login, string password)
        {
            _usersRepository.UpdateCredentials(login, password);
        }
    }
}
