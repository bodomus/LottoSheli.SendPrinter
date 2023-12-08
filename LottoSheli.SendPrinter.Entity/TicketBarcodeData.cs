using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    public class TicketBarcodeData
    {
        /// <summary>
        /// user id (4 last digits)
        /// </summary>
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// rows with numbers selected
        /// </summary>
        [JsonProperty("rows")]
        public IEnumerable<TicketTable> Tables { get; set; }

        /// <summary>
        /// integer array with 'extra' numbers
        /// </summary>
        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public int[] Extra { get; set; }

        /// <summary>
        /// integer representation of extra mark
        /// </summary>
        [JsonProperty("extra_mark", NullValueHandling = NullValueHandling.Ignore)]
        public int? ExtraMark { get; set; }
    }
}
