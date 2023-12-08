using LiteDB;
using Microsoft.Extensions.Logging;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    public class CommonRepositoryLiteBD : ICommonRepository
    {
        private readonly ILiteDatabase _context;
        private readonly ILogger<CommonRepositoryLiteBD> _logger;

        public CommonRepositoryLiteBD(IDBCreatorFactory dBCreatorFactory, ILogger<CommonRepositoryLiteBD> logger)
        {
            _context = dBCreatorFactory.GetDBContext(DBType.Scanqueue);
            _logger = logger;
        }

        public void VacuumStorage()
        {
            _context.Checkpoint();
        }
    }
}
