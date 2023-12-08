using LiteDB;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace LottoSheli.SendPrinter.Repository.LiteDB.Converters
{
    public class DrawConverter : IDrawConverter
    {
        public IList<Draw> Convert(string json)
        {
            var converted = JsonConvert.DeserializeObject<DataObject<DrawDataObject>>(json);
            var result = converted.Data?.Game?.AsParallel().AsOrdered();

            //result?.ForAll(obj => {
            //     obj.Settings = ToDictionary(obj.Settings);
            // });

            return result?.ToList();
        }

        private IDictionary<string, dynamic> ToDictionary(JToken token)
        {
            IDictionary<string, dynamic> result = new Dictionary<string, dynamic>();

            var dict = token is JArray ? null : token.ToObject<Dictionary<string, BsonValue>>();

            if (dict == null)
                return null;

            foreach (var item in dict.ToList())
            {
                result.Add(item.Key, item.Value);

                if (token[item.Key] != null && token[item.Key].Type == JTokenType.Object)
                {
                    dynamic innerResult = ToDictionary(token[item.Key]);
                    //if (innerResult == null)
                    //    innerResult = BsonValue.Null;

                    result[item.Key] = innerResult;
                }
                else if (token[item.Key] != null && token[item.Key].Type == JTokenType.Integer)
                {
                    result[item.Key] = result[item.Key].AsInt32;
                }
                else if (token[item.Key] != null && token[item.Key].Type == JTokenType.Float)
                {
                    result[item.Key] = result[item.Key].AsDouble;
                }
            }

            return result;
        }
    }
}
