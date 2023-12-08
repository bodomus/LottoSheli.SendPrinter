using Newtonsoft.Json;
using System.IO;

namespace LottoSheli.SendPrinter.Settings
{
    public class SettingsHelper
    {
        public T LoadJson<T>(string fileName)
        {
            T res;
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                res = JsonConvert.DeserializeObject<T>(json);
            }
            return res;
        }

        public void SaveJson<T>(string fileName, T sourse)
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, sourse);
            }
        }
    }
}
