using Newtonsoft.Json;
using System;

namespace LottoSheli.SendPrinter.Core.JsonConverters
{
    public class StringIntConverter : JsonConverter
    {
        /// <summary>
        /// writes 1 if value is true and 0 if false
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value == null ? "0" : value.ToString());
        }

        /// <summary>
        /// returns true if reader.Value equals to 1
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            int val;
            return int.TryParse(reader.Value?.ToString(), out val) ? val : 0;
        }

        /// <summary>
        /// returns true if objectType is boolean
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
