using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System.Collections.Generic;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveTaskCommandData : ICommandData
    {
        /// <summary>
        /// Id of the ticket to be removed
        /// </summary>
        public int Id { get; set; }
    }
}
