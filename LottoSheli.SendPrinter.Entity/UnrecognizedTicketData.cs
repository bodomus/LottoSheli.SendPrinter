using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Drawing;

namespace LottoSheli.SendPrinter.Entity
{
    /// <summary>
    /// Provides entity for urecognized ticket
    /// </summary>
    public class UnrecognizedTicketData : BaseEntity
    {
        public override int Id { get; set; }
        
        public override DateTime CreatedDate { get; set; }

        public TicketTask RecognizedTask { get; set; }

        public SlipDataEntity SlipData { get; set; }

        public TicketMatchingError MatchingError { get; set; }

        public UnrecognizedTicketError ErrorData { get; set; }

        public bool IsOrdered { get; set; } = false;
    }
}
