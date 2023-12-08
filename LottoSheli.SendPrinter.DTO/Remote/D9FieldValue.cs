using Newtonsoft.Json;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    public class D9FieldValue<T>
    {
        [JsonProperty(PropertyName = "value")]
        public T Value { get; set; }
    }

    public class D9UrlFieldValue<T>
    {
        [JsonProperty(PropertyName = "uri")]
        public T Value { get; set; }
    }
}
