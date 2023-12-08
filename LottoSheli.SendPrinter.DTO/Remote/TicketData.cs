using Newtonsoft.Json;
using System;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    public class TicketData
    {
        [JsonProperty(PropertyName = "iid")]
        public int IId { get; set; }

        [JsonProperty(PropertyName = "tid")]
        public int? TId { get; set; }

        [JsonProperty(PropertyName = "national_id")]
        public int? UId { get; set; }

        [JsonProperty(PropertyName = "game_type")]
        public string GameType { get; set; }

        [JsonProperty(PropertyName = "game_subtype")]
        public string GameSubType { get; set; }

        [JsonProperty(PropertyName = "extra")]
        public bool Extra { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime? Date  { get; set; }

        [JsonProperty(PropertyName = "uid_mandatory_flag")]
        public bool UidMandatoryFlag { get; set; }
    }
}
