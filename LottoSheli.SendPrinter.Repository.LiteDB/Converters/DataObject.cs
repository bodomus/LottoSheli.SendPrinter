using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Repository.LiteDB.Converters
{
    class DataObject<TData>
    {
        public string Type { get; set; }
        public TData Data { get; set; }
    }
}
