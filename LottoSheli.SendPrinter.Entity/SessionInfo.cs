using LottoSheli.SendPrinter.Entity.Enums;
using System;

namespace LottoSheli.SendPrinter.Entity
{
    public class SessionInfo : BaseEntity
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Token { get; set; }
        public override DateTime CreatedDate { get; set; }
        public string Session { get; set; }
        public Role ServerType { get; set; }
        public bool IsEmpty => string.IsNullOrEmpty(Token);
        public SessionInfo() 
        { 
            CreatedDate = DateTime.Now;
        }
    }
}
