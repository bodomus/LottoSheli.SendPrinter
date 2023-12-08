using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;

namespace LottoSheli.SendPrinter.Repository
{
    public interface IUserRepository : IBaseRepository<UserData>
    {
        UserData GetByUserRole(Role role);
        void UpdateCredentials(string login, string pass = "", Role role = Role.User);
        (string Login, string Password) GetCredentials(Role role = Role.User);
    }
}