using LiteDB;
using System;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    public interface IDBCreatorFactory : IAsyncDisposable, IDisposable
    {
        ILiteDatabase GetDBContext(DBType type);
        ILiteDatabase BackupDBContextToJson(DBType type);
        Task BackupDBContextToFile(DBType type);
        ILiteDatabase SyncDBContext(DBType type);
        bool IsDisposed { get; }
        event EventHandler<DBType> ContextChanged;
    }
}
