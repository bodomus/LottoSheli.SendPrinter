using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketPrintType
    {
        [EnumMember(Value = "range")]
        Range,
        [EnumMember(Value = "all")]
        All
    }
}
