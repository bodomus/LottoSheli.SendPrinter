using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LottoSheli.SendPrinter.App.ui.login;
using LottoSheli.SendPrinter.App.View;

namespace LottoSheli.SendPrinter.App.ui
{
    public interface IFormFactory
    {
        ILoginView CreateLoginForm();
     
    }
}
