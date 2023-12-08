using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace LottoSheli.SendPrinter.Entity.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DrawGameType
    {
        Undefined = -1,

        [EnumMember(Value = "lotto")]
        Lotto,

        [EnumMember(Value = "chance")]
        Chance,

        [EnumMember(Value = "777")]
        TripleSeven,

        [EnumMember(Value = "123")]
        The123,

        [EnumMember(Value = "social")]
        Social
    }
}
