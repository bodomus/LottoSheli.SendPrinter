using LottoSheli.SendPrinter.Repository;
using System.Windows.Forms;
using LottoSheli.SendPrinter.Core.Enums;
using LottoSheli.SendPrinter.App.ui.login;
using Microsoft.Extensions.DependencyInjection;


namespace LottoSheli.SendPrinter.App.Presenter
{
    public class LoginPresenter
    {
        private ILoginView view;

        private IUserRepository _usersRepository;
        public LoginPresenter(ILoginView loginView, IServiceProvider serviceProvider)
        {
            view = loginView;
            _usersRepository = serviceProvider.GetRequiredService<IUserRepository>();
            //loginView.Presenter = this;
            view.ModeSelectedIndex = 1;
            view.UcLoginComponent.RightToLeftDirectionCheckboxVisible = false;
        }

        public (RightToLeft RightToLeft, ScannerMode Mode) GetResult()
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
