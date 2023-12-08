using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    [JsonObject(
        ItemNullValueHandling = NullValueHandling.Ignore,
        MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class TicketPayloadData : BaseEntity
    {
        [JsonProperty("id")]
        public override int Id { get; set; }

        [JsonProperty("sequence")]
        public int Sequence { get; set; }

        [JsonProperty("data")]
        public ScannerResponseData Data {get; set;}

        [JsonProperty("create_date")]
        public override DateTime CreatedDate { get; set; }
    }
}
