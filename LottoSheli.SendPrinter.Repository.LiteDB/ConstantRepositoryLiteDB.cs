using LiteDB;
using LottoSheli.SendPrinter.Entity;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    /// <summary>
    /// LiteDB implementation of <see cref="IConstantRepository"/> interface.
    /// </summary>
    public class ConstantRepositoryLiteDB : BaseRepositoryLiteDB<Constant>, IConstantRepository
    {
        private ILogger<ConstantRepositoryLiteDB> _logger;

        public ConstantRepositoryLiteDB(IDBCreatorFactory dBCreatorFactory, ILogger<ConstantRepositoryLiteDB> logger) : base(dBCreatorFactory)
        {
            _logger = logger;
            Context.GetCollection<Constant>(EntityStorageName).EnsureIndex(obj => obj.ConstantName, true);
        }

        protected override string EntityStorageName => "db_constants";

        public override Constant CreateNew()
        {
            return new Constant();
        }

        public virtual Constant GetByName(string constantName)
        {
            var db = Context.GetCollection<Constant>(EntityStorageName);

            return db.Find(obj => obj.ConstantName == constantName).FirstOrDefault();
        }
    }
}
