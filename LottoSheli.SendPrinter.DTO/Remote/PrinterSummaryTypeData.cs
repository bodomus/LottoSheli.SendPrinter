using Newtonsoft.Json;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    public class PrinterSummaryTypeData
    {
        [JsonProperty(PropertyName = "target_id")]
        public string TargetId { get; set; }

        [JsonProperty(PropertyName = "target_type")]
        public string TargetType { get; set; }
    }
}
