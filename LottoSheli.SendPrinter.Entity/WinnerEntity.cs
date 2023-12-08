using LottoSheli.SendPrinter.Core.JsonConverters;
using LottoSheli.SendPrinter.Entity.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Entity
{
    public class WinnerEntity : BaseEntity
    {
        /// <summary>
        /// Draw ID
        /// </summary>
        [JsonProperty("id")]
        public override int Id { get; set; }

        /// <summary>
        /// Entity Creation Date
        /// </summary>
        [JsonIgnore]
        public override DateTime CreatedDate { get; set; }

        public Winner Winner { get; set; }
    }

    public class WinnersData
    {
        [JsonProperty("pais_id")]
        public string PaisId { get; set; }

        [JsonProperty("ticket_type")]
        public DrawGameType DrawType { get; set; }

        [JsonProperty("data")]
        public IEnumerable<Winner> Winners { get; set; }
    }

    public class Winner : TicketTask
    {
        [JsonProperty("serial")]
        public int Serial { get; set; }

        [JsonProperty("package")]
        public int Package { get; set; }

        [JsonProperty("slip_id")]
        public string SlipId { get; set; }

        /// <summary>
        /// ticket price
        /// </summary>
        [JsonProperty("amount")]
        [JsonConverter(typeof(StringDoubleConverter))]
        public double AmountWon { get; set; }
    }
}
