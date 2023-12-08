using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace LottoSheli.SendPrinter.Entity.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TaskSubType
    {
        [EnumMember(Value = "regular")]
        LottoRegular,

        [EnumMember(Value = "double")]
        LottoDouble,

        [EnumMember(Value = "social")]
        LottoSocial,

        [EnumMember(Value = "methodical")]
        LottoMethodical,

        [EnumMember(Value = "strong_methodical")]
        LottoStrongMethodical,

        [EnumMember(Value = "double_methodical")]
        LottoDoubleMethodical,

        [EnumMember(Value = "double_strong_methodical")]
        LottoDoubleStrongMethodical,

        [EnumMember(Value = "777")]
        TripleSeven,

        [EnumMember(Value = "777_methodical")]
        TripleSevenMethodical,

        [EnumMember(Value = "regular_chance")]
        RegularChance,

        [EnumMember(Value = "multiple_chance")]
        MultipleChance,

        [EnumMember(Value = "methodical_chance")]
        MethodicalChance,

        [EnumMember(Value = "123")]
        The123,

        Undefined = -1
    }
}
