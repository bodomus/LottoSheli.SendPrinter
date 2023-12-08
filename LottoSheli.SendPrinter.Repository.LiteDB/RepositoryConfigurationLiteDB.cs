using LiteDB;
using LiteDB.Engine;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    public class RepositoryConfigurationLiteDB : IRepositoryConfiguration
    {
        public void UnprotectSacnqueueDB(string tempLiteDbFile)
        {
            if (!DBCreatorFactory.IsDbPasswordProtected(tempLiteDbFile))
                return;

            var scanDbConnection = new ConnectionString(tempLiteDbFile) { Password = "scanlist_pass" };
            using LiteDatabase db = new LiteDatabase(scanDbConnection);
            db.Rebuild(new RebuildOptions() { Password = null });
        }
    }
}
