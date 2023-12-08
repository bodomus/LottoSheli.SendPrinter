using LottoSheli.SendPrinter.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace LottoSheli.SendPrinter.Entity.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TaskType
    {
        [TicketType("Undefined")]
        [EnumMember(Value = "")]
        Undefined,

        [TicketType("Lotto")]
        [EnumMember(Value = "regular")]
        LottoRegular,

        [TicketType("Lotto")]
        [EnumMember(Value = "double")]
        LottoDouble,

        [TicketType("Lotto")]
        [EnumMember(Value = "social")]
        LottoSocial,

        [TicketType("777")]
        [EnumMember(Value = "777")]
        TripleSeven,

        [TicketType("Chance")]
        [EnumMember(Value = "regular_chance")]
        [TaskTypeSummaryName("reg_chance")]
        RegularChance,

        [TicketType("Chance")]
        [EnumMember(Value = "multiple_chance")]
        [TaskTypeSummaryName("multi_chance")]
        MultipleChance,

        [TicketType("Chance")]
        [EnumMember(Value = "methodical_chance")]
        [TaskTypeSummaryName("meth_chance")]
        MethodicalChance,

        [TicketType("123")]
        [EnumMember(Value = "regular_123")]
        Regular123,

        [TicketType("123")]
        [EnumMember(Value = "combined_123")]
        Combined123
    }
}
