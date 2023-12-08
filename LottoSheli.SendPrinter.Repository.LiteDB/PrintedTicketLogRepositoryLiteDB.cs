using LottoSheli.SendPrinter.Entity;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    /// <summary>
    /// LiteDB implementation of <see cref="IPrintedTicketLogRepository"/> interface.
    /// </summary>
    public class PrintedTicketLogRepositoryLiteDB : BaseRepositoryLiteDB<PrintedTicketLog>, IPrintedTicketLogRepository
    {
        private ILogger<ConstantRepositoryLiteDB> _logger;

        public PrintedTicketLogRepositoryLiteDB(IDBCreatorFactory dBCreatorFactory, ILogger<ConstantRepositoryLiteDB> logger) : base(dBCreatorFactory)
        {
            _logger = logger;
            Context.GetCollection<Constant>(EntityStorageName).EnsureIndex(obj => obj.ConstantName, true);
        }

        protected override string EntityStorageName => "printed_tickets";

        public override PrintedTicketLog CreateNew()
        {
            return new PrintedTicketLog();
        }
    }
}
