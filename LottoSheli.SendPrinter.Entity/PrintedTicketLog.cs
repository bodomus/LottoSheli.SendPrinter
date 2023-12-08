using System;

namespace LottoSheli.SendPrinter.Entity
{
    /// <summary>
    /// Represents entity of printed ticket logs
    /// </summary>
    public class PrintedTicketLog : BaseEntity
    {
        /// <summary>
        /// Ticket Id
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        /// Printed date
        /// </summary>
        public override DateTime CreatedDate { get; set; }
    }
}
