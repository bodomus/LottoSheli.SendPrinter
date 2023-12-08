using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, 
        ItemNullValueHandling = NullValueHandling.Ignore, 
        MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class WinnersData
    {
        [JsonProperty(PropertyName = "did")]
        public int DrawId { get; set; }

        [JsonProperty(PropertyName = "operator_uid")]
        public int? StationId { get; set; }
    }
}
