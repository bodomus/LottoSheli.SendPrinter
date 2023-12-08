using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Entity;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    /// <summary>
    /// LiteDB implementation of <see cref="ITicketTaskRepository"/> interface.
    /// </summary>
    public class TicketTaskRepositoryLiteDB : TicketTaskRepositoryBaseLiteDB<TicketTask>, ITicketTaskRepository
    {
        private IHashGenerator _hashGenerator;
        private ILogger<TicketTaskRepositoryLiteDB> _logger;

        public override DBType EntityStorageType => DBType.Tickets;

        public TicketTaskRepositoryLiteDB(IDBCreatorFactory dBCreatorFactory, IHashGenerator hashGenerator, ILogger<TicketTaskRepositoryLiteDB> logger) 
            : base(dBCreatorFactory, hashGenerator, logger)
        {
            _hashGenerator= hashGenerator;
            _logger = logger;
            //disabled tempoprary becuase of LOTIL-4801
            //Context.GetCollection<TicketTask>(EntiityStorageName).EnsureIndex(obj => obj.TablesHash, true);
            //Hash = TablesArray + NID

        }

        protected override string EntityStorageName => "scanqueue";

        public override TicketTask CreateNew()
        {
            return new TicketTask();
        }

    }
}
