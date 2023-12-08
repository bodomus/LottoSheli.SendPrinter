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
    public class D7SessionData
    {
        [JsonProperty(PropertyName = "session_name")]
        public string SessionName { get; set; }

        [JsonProperty(PropertyName = "sessid")]
        public string SessionValue { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string CsrfToken { get; set; }

        [JsonIgnore]
        public bool IsAutomaticPrinter => false;

        [JsonProperty(PropertyName = "uid")]
        public string StationId { get; set; }

    }
}
