using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Core.JsonConverters
{
    public class StringDoubleConverter : JsonConverter
    {
        /// <summary>
        /// writes stringified value or 0.0 if null
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(null == value ? "0.0" : value.ToString());
        }

        /// <summary>
        /// returns parsing result or 0.0
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return double.TryParse(reader.Value?.ToString(), out double price) ? price : 0.0;
        }

        /// <summary>
        /// returns true if objectType is string
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
