using LottoSheli.SendPrinter.Entity.Enums;
using System;

namespace LottoSheli.SendPrinter.Entity
{

    public class UserData : BaseEntity
    {
        public override int Id { get; set; }
        public override DateTime CreatedDate { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public Role Role { get; set; }
    }
}
