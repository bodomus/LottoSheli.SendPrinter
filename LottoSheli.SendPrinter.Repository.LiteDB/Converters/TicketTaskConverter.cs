using LiteDB;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LottoSheli.SendPrinter.Repository.LiteDB.Converters
{
    public class TicketTaskConverter : ITicketTaskConverter
    {
        public TicketTask Convert(string json)
        {
            return ProcessTicketTaskResponse(json);
        }

        public IEnumerable<TicketTask> ConvertMany(string json)
        {
            return ProcessMultiTicketTaskResponse(json);
        }

        private static bool TryGetIntValue(BsonValue bsonValue, out int result)
        {
            try
            {
                result = bsonValue.AsInt32;
                return true;
            }
            catch {
                result = -1;
                return false;
            }
        }

        private IDictionary<string, dynamic> ToDictionary(JToken token)
        {
            IDictionary<string, dynamic> result = new Dictionary<string, dynamic>();

            var dict = token.ToObject<Dictionary<string, BsonValue>>();

            if (dict == null)
                return null;

            foreach (var item in dict.ToList())
            {
                if(TryGetIntValue(item.Value, out int intValue))
                    result.Add(item.Key, intValue);
                else
                    result.Add(item.Key, item.Value);

                if (token[item.Key]?.Type == JTokenType.Object)
                {
                    dynamic innerResult = ToDictionary(token[item.Key]);
                    result[item.Key] = innerResult;
                }
                else if (token[item.Key]?.Type == JTokenType.Float)
                {
                    result[item.Key] = result[item.Key].AsDouble;
                }
            }

            return result;
        }

        private TicketTask ProcessTicketTaskResponse(string response)
        {
            var result = JsonConvert.DeserializeObject<DataObject<IList<TicketTask>>>(response);

            if (result?.Data?.Any() == true)
            {
                var item = result.Data.First();
                item.Settings = ToDictionary(item.Settings);
                return item;
            }

            return null;
        }

        private IEnumerable<TicketTask> ProcessMultiTicketTaskResponse(string response)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<DataObject<IList<TicketTask>>>(response);
                foreach(var item in result.Data)
                    item.Settings = ToDictionary(item.Settings);
                return result.Data;
            }
            catch 
            { 
                return new List<TicketTask>();
            }
        }
    }
}
