using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    public class ServerData<T>
    {
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}
