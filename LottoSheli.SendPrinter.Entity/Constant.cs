using LottoSheli.SendPrinter.Core.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    /// <summary>
    /// Provides entity for Ticket task
    /// </summary>
    public class Constant : BaseEntity
    {
        /// <summary>
        /// UID
        /// </summary>
        [JsonIgnore]
        public override int Id { get; set; }

        /// <summary>
        /// Constant Name
        /// </summary>
        [JsonIgnore]
        public string ConstantName { get; set; }


        /// <summary>
        /// Constant Value
        /// </summary>
        [JsonIgnore]
        public string ConstantValue { get; set; }


        public override DateTime CreatedDate { get; set; }
    }
}
