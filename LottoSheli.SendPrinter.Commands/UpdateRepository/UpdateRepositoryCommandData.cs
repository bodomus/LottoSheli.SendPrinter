using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System.Collections.Generic;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands
{
    /// <summary>
    /// The date for <see cref="IRollbackSendingTaskBySequenceCommand"/>
    /// </summary>
    public class UpdateRepositoryCommandData : ICommandData
    {
        /// <summary>
        /// Ticket sequence number
        /// </summary>
        public int Version { get; init; }
    }
}
