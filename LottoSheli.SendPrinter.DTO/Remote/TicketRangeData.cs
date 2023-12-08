using Newtonsoft.Json;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    public class TicketRangeData
    {
        [JsonProperty(PropertyName = "from")]
        public int From { get; set; }

        [JsonProperty(PropertyName = "to")]
        public int To { get; set; }
    }
}
