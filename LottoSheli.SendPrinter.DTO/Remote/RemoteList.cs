using System.Collections.Generic;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    public class RemoteList<TData>
    {
        public string Type { get; set; }
        public IList<TData> Data { get; set; }
    }
}
