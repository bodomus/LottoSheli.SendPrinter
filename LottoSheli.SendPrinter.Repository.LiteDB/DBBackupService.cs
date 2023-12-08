using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    public interface IDBBackupService 
    {
        void BackupDatabase(ConnectionString connString);
        Task BackupDatabaseAsync(ConnectionString connString);
    }
    public class DBBackupService : IDBBackupService
    {
        private const string BCK_DIR = "Backups";
        private const int BCK_TTL = 24;
        private string _ts => DateTime.Now.ToString("yyyy-MM-dd-HH-mm");

        public Func<FileInfo, bool> IsReadyForCleanup { get; set; } = (fi) => (DateTime.Now - fi.CreationTime).TotalHours > BCK_TTL;
        public void BackupDatabase(ConnectionString connString) 
        {
            if (connString is { Filename: var fname } && !string.IsNullOrEmpty(fname) && File.Exists(fname))
            {
                var bckName = Path.GetFileName(fname).Replace(".db", $"-{_ts}.db");
                var bckDir = Path.Combine(Directory.GetParent(fname).FullName, BCK_DIR);
                if (!Directory.Exists(bckDir))
                    Directory.CreateDirectory(bckDir);
                File.Copy(fname, Path.Combine(bckDir, bckName), true);
                Cleanup(connString);
            }
        }

        public Task BackupDatabaseAsync(ConnectionString connString) => Task.Run(() => BackupDatabase(connString));

        private void Cleanup(ConnectionString connString) 
        {
            if (connString is { Filename: var fname } && !string.IsNullOrEmpty(fname) && File.Exists(fname))
            {
                var bckDir = Path.Combine(Directory.GetParent(fname).FullName, BCK_DIR);
                if (Directory.Exists(bckDir)) 
                {
                    var di = new DirectoryInfo(bckDir);
                    di.EnumerateFiles()
                        .Where(fi => IsReadyForCleanup(fi))
                        .AsParallel()
                        .ForAll(fi => fi.Delete());
                }
                    
            }
        }
    }
}
