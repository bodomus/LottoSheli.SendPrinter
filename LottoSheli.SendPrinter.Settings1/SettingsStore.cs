using LiteDB;
using LiteDB.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Settings
{
    internal class SettingsItem 
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Json { get; set; }
    }
    public class SettingsStore
    {
        
        private ILiteDatabase _db;
        
        #region Pugh singletone implementation
        private SettingsStore()
        {
            _db = CreateDbContext();
        }

        public static SettingsStore Instance => PughSingletone.instance;

        private class PughSingletone
        {
            static PughSingletone() { }
            internal static readonly SettingsStore instance = new SettingsStore();
        }
        #endregion

        public bool HasSettings(string collectionName) 
        {
            _db.BeginTrans();
            var collection = _db.GetCollection<SettingsItem>(collectionName);
            _db.Commit();
            return collection.Count() > 0;
        }
        public TSettings GetSettings<TSettings>(string collectionName) 
        {
            _db.BeginTrans();
            var collection = _db.GetCollection<SettingsItem>(collectionName);
            var item = collection.FindAll().FirstOrDefault();
            _db.Commit();
            return null == item ? default(TSettings) : JsonConvert.DeserializeObject<TSettings>(item.Json);
        }
        public void SaveSettings<TSettings>(TSettings settings, string collectionName) 
        {
            _db.BeginTrans();
            var collection = _db.GetCollection<SettingsItem>(collectionName);
            var item = collection.FindAll().FirstOrDefault() ?? new SettingsItem { Name = collectionName };
            item.Json = JsonConvert.SerializeObject(settings);
            collection.Upsert(item);
            _db.Commit();
        }

        public void ClearSettings(string collectionName) 
        {
            _db.BeginTrans();
            if (_db.CollectionExists(collectionName))
                _db.GetCollection<SettingsItem>(collectionName).DeleteAll();
            _db.Commit();
        }
        
        private static ILiteDatabase CreateDbContext()
        {
            ConnectionString connectionString = new ConnectionString
            {
                Filename = Path.Combine(SettingsManager.LottoHome, "settings.db"),
                Password = "settings_pass",
                Connection = ConnectionType.Shared
            };
            ILiteDatabase db = new LiteDatabase(connectionString);
            db.CheckpointSize = 100;
            db.Rebuild(new RebuildOptions() { Password = connectionString.Password });
            db.Commit();

            return db;
        }

    }
}
