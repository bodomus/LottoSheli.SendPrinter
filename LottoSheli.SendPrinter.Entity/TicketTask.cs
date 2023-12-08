using LottoSheli.SendPrinter.Core.JsonConverters;
using LottoSheli.SendPrinter.Entity.Enums;
using LottoSheli.SendPrinter.Entity.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Entity
{
    /// <summary>
    /// Provides entity for Ticket task
    /// </summary>
    public class TicketTask : BaseEntity
    {
        /// <summary>
        /// always required task id
        /// </summary>
        [JsonProperty("id")]
        public override int Id { get; set; }

        /// <summary>
        /// always required game type
        /// </summary>
        [JsonProperty("type")]
        public TaskType Type { get; set; }

        /// <summary>
        /// game subtype
        /// </summary>
        [JsonProperty("subtype")]
        public TaskSubType SubType { get; set; }

        /// <summary>
        /// Draw Id
        /// </summary>
        [JsonProperty("did")]
        public int DrawId { get; set; }

        /// <summary>
        /// always required ticket id
        /// </summary>
        [JsonProperty("tid")]
        public int TicketId { get; set; }

        /// <summary>
        /// number of draws
        /// </summary>
        [JsonProperty("draws")]
        public int MultipleDraw { get; set; }


        /// <summary>
        /// ticket price
        /// </summary>
        [JsonProperty("price")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal Price { get; set; }

        /// <summary>
        /// national id (last 4 digits) as decimal
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// national id (last 4 digits) as hexademical
        /// </summary>
        [JsonIgnore]
        public string NationalId => NationalIdUtils.DecToHex(UserId);

        /// <summary>
        /// Flag denoting if extra mark is present
        /// </summary>
        [JsonProperty("extra")]
        [JsonConverter(typeof(IntBoolConverter))]
        public bool? Extra { get; set; }

        /// <summary>
        /// auto flag
        /// </summary>
        [JsonProperty("auto")]
        [JsonConverter(typeof(IntBoolConverter))]
        public bool? Auto { get; set; }

        /// <summary>
        /// user id mandatory flag
        /// </summary>
        [JsonProperty("user_id_mandatory_flag")]
        public int UserIdMandatoryFlag { get; set; }

        /// <summary>
        /// queue name
        /// </summary>
        [JsonProperty("queue_name")]
        public string QueueName { get; set; }

        /// <summary>
        /// Station ID for Durupal server
        /// </summary>
        [JsonProperty("drupal9_station_id")]
        public string PrintedStationId { get; set; }

        /// <summary>
        /// required always rows with numbers selected
        /// </summary>
        [JsonProperty("rows")]
        public IEnumerable<TicketTable> Tables { get; set; }

        /// <summary>
        /// bundle in which the ticket was printed
        /// </summary>
        [JsonIgnore]
        public int Bundle { get; set; } = -1;

        /// <summary>
        /// Tables computed hash
        /// </summary>
        [JsonIgnore]
        public string TablesHash { get; set; }

        /// <summary>
        /// settings
        /// </summary>
        [JsonProperty("settings")]
        public dynamic Settings { get; set; }

        /// <summary>
        /// Ticket order number
        /// </summary>
        [JsonIgnore]
        public int Sequence { get; set; } = -1;

        /// <summary>
        /// Printed Count
        /// </summary>
        [JsonIgnore]
        public int PrintedCount { get; set; }

        /// <summary>
        /// Ticket Created Date
        /// </summary>
        [JsonIgnore]
        public override DateTime CreatedDate { get; set; }

        public int? HashVersion { get; set; }
    }
}
