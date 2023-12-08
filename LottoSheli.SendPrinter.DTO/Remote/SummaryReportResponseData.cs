using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    public class SummaryReportResponseData
    {
        [JsonIgnore]
        public long? Id
        {
            get
            {

                return IdField?.FirstOrDefault()?.Value;

            }
            set
            {
                if (IdField == null)
                    IdField = new List<D9FieldValue<long?>> { new D9FieldValue<long?> { Value = value } };
                else
                    IdField[0].Value = value;

            }
        }

        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        internal IList<D9FieldValue<long?>> IdField { get; private set; }
    }
}
