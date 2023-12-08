using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;

namespace LottoSheli.SendPrinter.Repository
{
    public interface ISessionRepository : IBaseRepository<SessionInfo>
    {
        SessionInfo GetSession(Role type);

        void UpsertSession(SessionInfo session);

        void ClearSession(Role type);

        bool HasSession(Role type);
    }
}
