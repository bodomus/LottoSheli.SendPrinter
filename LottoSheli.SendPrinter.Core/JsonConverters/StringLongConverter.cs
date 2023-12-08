using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LottoSheli.SendPrinter.Core.JsonConverters
{
    public class StringLongConverter : JsonConverter
    {
        /// <summary>
        /// writes stringified value or 0.0 if null
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(null == value ? "0" : value.ToString());
        }

        /// <summary>
        /// returns parsing result or 0
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return long.TryParse(reader.Value?.ToString(), out long val) ? val : 0;
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
