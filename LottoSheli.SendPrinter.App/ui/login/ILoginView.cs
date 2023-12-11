using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LottoSheli.SendPrinter.App.Presenter;
using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Core.Controls;
using LottoSheli.SendPrinter.Core.Enums;

namespace LottoSheli.SendPrinter.App.ui.login
{
    public interface ILoginView
    {
        int ModeSelectedIndex { get; set; }

        //int VersionSelectedIndex { get; set; }

        ScannerMode ScannerMode { get; }

        UCLogin UcLoginComponent { get; }

        bool RightToLeftDirectionCheckboxVisible { get; set; }

        void CloseDialog();

        void RejectDialog();

        //public string Password { get; set; }
        //public string Login { get; set; }

        //public Color MessageForeColor { get; set; }

        event EventHandler<AuthorizationEventArgs> UpdateCredential;

        event EventHandler<AuthorizationEventArgs> ReceiveCredential;

        LoginPresenter Presenter { get; set; }

    }
}
