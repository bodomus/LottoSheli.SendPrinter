using LiteDB;
using LottoSheli.SendPrinter.Entity;
using Microsoft.Extensions.Logging;
using System;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    public class WinnersRepositoryLiteDB : BaseRepositoryLiteDB<WinnerEntity>, IWinnersRepository
    {
        private ILogger<WinnersRepositoryLiteDB> _logger;

        public WinnersRepositoryLiteDB(IDBCreatorFactory dBCreatorFactory, ILogger<WinnersRepositoryLiteDB> logger) : base(dBCreatorFactory)
        {
            _logger = logger;
            Context.GetCollection<WinnerEntity>(EntityStorageName).EnsureIndex(obj => obj.Id, true);
        }

        public override DBType EntityStorageType { get; } = DBType.Winners;

        protected override string EntityStorageName => "winners";

        public override WinnerEntity CreateNew()
        {
            return new WinnerEntity() { CreatedDate = DateTime.Now };
        }
    }
}
