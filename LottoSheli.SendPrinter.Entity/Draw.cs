using LottoSheli.SendPrinter.Entity.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LottoSheli.SendPrinter.Entity
{
    /// <summary>
    /// Provides entity for Draw
    /// </summary>
    public class Draw : BaseEntity
    {
        /// <summary>
        /// Draw ID
        /// </summary>
        [JsonProperty("did", Required = Required.Always)]
        public override int Id { get; set; }

        /// <summary>
        /// Sale Start Date
        /// </summary>
        [JsonProperty("sale_start_date", ItemConverterType = typeof(UnixDateTimeConverter))]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime SaleStartDate { get; set; }

        /// <summary>
        /// Sale End Date
        /// </summary>
        [JsonProperty("sale_end_date", ItemConverterType = typeof(UnixDateTimeConverter))]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime SaleEndDate { get; set; }

        /// <summary>
        /// Draw Date
        /// </summary>
        [JsonProperty("draw_date", ItemConverterType = typeof(UnixDateTimeConverter))]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime DrawDate { get; set; }

        /// <summary>
        /// Game Type
        /// </summary>
        [JsonProperty("game_type", Required = Required.Always)]
        public DrawGameType Type { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status", Required = Required.Always)]
        public int Status { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Ticket Printed count
        /// </summary>
        [JsonProperty("printed_amount")]
        public int PrintedAmount { get; set; }

        /// <summary>
        /// Ticket scanned count
        /// </summary>
        [JsonProperty("processed_amount")]
        public int ProcessedAmount { get; set; }

        /// <summary>
        /// Station Name
        /// </summary>
        [JsonProperty("station_name")]
        public string StationName { get; set; }

        /// <summary>
        /// Entity Creation Date
        /// </summary>
        [JsonIgnore]
        public override DateTime CreatedDate { get; set; }
    }
}
