using LiteDB;
using LiteDB.Engine;
using LottoSheli.SendPrinter.Settings;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    public class DBCreatorFactory : IDBCreatorFactory
    {
        private const int DB_EXPIRY_MINUTES = 10;
        private const int DB_CHECKPOINT_LIMIT = 100;
        private static ConcurrentDictionary<DBType, ILiteDatabase> _cachedDB = new ConcurrentDictionary<DBType, ILiteDatabase>();
        private static ConcurrentDictionary<DBType, DateTime> _cacheExpiry = new ConcurrentDictionary<DBType, DateTime>();
        private static Dictionary<DBType, object> _lockers = new Dictionary<DBType, object>();
        private int _disposed = 0;

        private readonly IDBBackupService _backupService;
        private string _ts => DateTime.Now.ToString("yyyy-MM-dd-HH-mm");

        public bool IsDisposed => _disposed > 0;

        public event EventHandler<DBType> ContextChanged;

        public DBCreatorFactory(IDBBackupService backupService)
        {
            _backupService = backupService;
            InitLockers();
            //InitDbCache();
        }

        public ILiteDatabase GetDBContext(DBType type = DBType.Scanqueue)
        {
            // to avoid race condition on db engine rotation we need to lock operation
            lock(_lockers[type])
            {
                if (_cachedDB.TryGetValue(type, out ILiteDatabase cachedDb))
                {
                    if (_cacheExpiry.TryGetValue(type, out DateTime expiry) && expiry > DateTime.Now)
                        return cachedDb;
                    cachedDb.Commit();
                    cachedDb.Checkpoint();
                    cachedDb.Dispose();
                }

                ILiteDatabase db = CreateDbContext(type);
                DateTime expiryDate = DateTime.Now.AddMinutes(DB_EXPIRY_MINUTES);
                _cachedDB.AddOrUpdate(type, db, (t, odb) => db);
                _cacheExpiry.AddOrUpdate(type, expiryDate, (t, oed) => expiryDate);

                return db;
            }
        }

        private ILiteDatabase CreateDbContext(DBType type)
        {
            ConnectionString connectionString = GetConnectionString(type);
            ILiteDatabase db = new LiteDatabase(connectionString);
            db.CheckpointSize = 100;
            db.Rebuild(new RebuildOptions() { Password = connectionString.Password });
            db.Commit();

            return db;
        }

        private void InitLockers() 
        {
            foreach (DBType type in Enum.GetValues(typeof(DBType)))
                _lockers.TryAdd(type, new object());
        }

        private void InitDbCache()
        {
            foreach (DBType type in Enum.GetValues(typeof(DBType)))
                GetDBContext(type);
        }

        public static bool IsDbPasswordProtected(string path)
        {
            if(!File.Exists(path))
                return false;
            using FileStream fs = File.OpenRead(path);
            return fs.ReadByte() > 0;
        }

        public ILiteDatabase BackupDBContextToJson(DBType type = DBType.Scanqueue) 
        {
            lock (_lockers[type]) 
            {
                var db = SyncDBContext(type);
                if (null != db) 
                {
                    var connString = GetConnectionString(type);
                    var dbBackupName = connString.Filename.Replace(".db", $"-{_ts}.json");
                    db.BeginTrans();
                    db.Execute($"SELECT $ INTO $FILE('{dbBackupName}')");
                }
                return db;
            }
        }

        public Task BackupDBContextToFile(DBType type = DBType.Scanqueue) => Task.Run(() => BackupDBContextToFileSync(type));

        public void BackupDBContextToFileSync(DBType type = DBType.Scanqueue)
        {
            lock (_lockers[type]) 
            {
                var db = SyncDBContext(type);
                if (null != db)
                {

                    if (_cachedDB.ContainsKey(type) && !_cachedDB.TryRemove(type, out db))
                        throw new InvalidOperationException($"Failed to remove database {type} from cache");
                    db.Dispose();
                    BackupDBFile(type);
                }
            }
        }

        public ILiteDatabase SyncDBContext(DBType type = DBType.Scanqueue)
        {
            if (_cachedDB.TryGetValue(type, out ILiteDatabase db))
            {
                db.Commit();
                db.Checkpoint();
                // Rebuilding helps reducing database file size. But for now it doesn't look as issue
                // therefore commented for the sake of performance
                // var password = GetConnectionString(type).Password;
                // db.Rebuild(new RebuildOptions { Password = password });
                return db;
            }
            return null;
        }

        private ConnectionString GetConnectionString(DBType type = DBType.Scanqueue) => type switch 
        { 
            DBType.Scanqueue => CreateConnectionString("scanlist.db", "scanlist_pass", ConnectionType.Direct),
            DBType.Winners => CreateConnectionString("winners.db", "winners_pass", ConnectionType.Direct),
            DBType.Credentials => CreateConnectionString("creds.db", "creds_pass", ConnectionType.Shared),
            DBType.Tickets => CreateConnectionString("tickets.db", "tickets_pass", ConnectionType.Direct),
            DBType.Jobs => CreateConnectionString("jobs.db", "jobs_pass", ConnectionType.Direct),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        private ConnectionString CreateConnectionString(string filename, string password, ConnectionType connType = ConnectionType.Shared) => new ConnectionString
        {
            Filename = Path.Combine(SettingsManager.LottoHome, filename),
            Password = password,
            Connection = connType
        };

        private Task BackupDBFileAsync(DBType type = DBType.Scanqueue) => Task.Run(() => BackupDBFile(type));

        private void BackupDBFile(DBType type = DBType.Scanqueue) 
        {
            var connString = GetConnectionString(type);
            _backupService.BackupDatabase(connString);
        }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (0 == _disposed && disposing)
            {
                Interlocked.Exchange(ref _disposed, 1);
                _cachedDB.AsParallel().Select(obj =>
                {
                    (obj.Value as IDisposable)?.Dispose();
                    return true;
                }).ToArray();
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable

        #region IAsyncDisposable
        private async ValueTask DisposeAsyncCore()
        {
           var result = _cachedDB.AsParallel().Select(obj => 
                    Task.Run(() => (obj.Value as IDisposable)?.Dispose())).ToArray();

            await Task.WhenAll(result);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();

            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
