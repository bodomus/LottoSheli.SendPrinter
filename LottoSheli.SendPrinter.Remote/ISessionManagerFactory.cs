using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    public interface ISessionManagerFactory
    {
        SessionManager GetSessionManager(Role serverRole);
        Task ResetAllActiveSessions();
    }
}
