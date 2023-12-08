using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using LottoSheli.SendPrinter.Core.JsonConverters;


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
        [Newtonsoft.Json.JsonIgnore]
        public override int Id { get; set; }

        /// <summary>
        /// Constant Name
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string ConstantName { get; set; }


        /// <summary>
        /// Constant Value
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string ConstantValue { get; set; }


        public override DateTime CreatedDate { get; set; }
    }
}
