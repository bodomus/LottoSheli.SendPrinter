using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    /// <summary>
    /// Provides entity for urecognized ticket
    /// </summary>
    public class UnrecognizedTicketError
    {
        /// <summary>
        /// Error name.
        /// </summary>
        public string ErrorName { get; set; }

        /// <summary>
        /// Error mssage.
        /// </summary>
        public string ErrorMessage{ get; set; }
    }
}
