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
    public class D9LoginData
    {
        [JsonProperty(PropertyName = "access_key")]
        public string AccessKey { get; set; }
    }
}
