using Newtonsoft.Json;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    public class TargetValue
    {
        [JsonProperty(PropertyName = "target_id", Required = Required.Always)]
        public int? TargetId { get; set; }

        [JsonProperty(PropertyName = "target_type", Required = Required.Always)]
        public string TargetName { get; set; }
    }
}
