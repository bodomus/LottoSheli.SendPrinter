using LottoSheli.SendPrinter.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    public class ScannedSlipRepositoryLiteDB : BaseRepositoryLiteDB<ScannedSlip>, IScannedSlipRepository
    {
        private const int DEFAULT_LIMIT = 1000;
        private readonly ILogger<IScannedSlipRepository> _logger;
        private int _limit = -1;
        protected override string EntityStorageName => "scanned_slips";

        public int Limit { get => _limit; set => SetLimit(value); }

        public bool IsEmpty => 0 == Collection.Count();

        public ScannedSlipRepositoryLiteDB(IDBCreatorFactory dBCreatorFactory, ILogger<IScannedSlipRepository> logger) : base(dBCreatorFactory) 
        { 
            _logger = logger;
            _limit = DEFAULT_LIMIT;
            Collection.EnsureIndex<string>((slip) => slip.SlipId, true);
        }

        public override ScannedSlip CreateNew()
        {
            return new ScannedSlip();
        }

        public bool HasSlip(string slipId) 
        { 
            try 
            {
                // need to split expression into separate ones because LiteDB fails to arrange separate txs in such a case
                int count = Count();
                ScannedSlip storedSlip = Collection.FindOne(slip => slip.SlipId == slipId);

                return count > 0 && null != storedSlip; 
            }
            catch(Exception ex)
            {
                _logger.LogError($"Scanned slip query failed due to ${ex.Message}");
                return false; 
            }
        }

        public ScannedSlip GetSlip(string slipId) => Collection.FindOne(slip => slip.SlipId == slipId);

        public void Upsert(ScannedSlip slip) 
        { 
            Collection.Upsert(slip);
            SetLimit(Limit);
            _logger.LogInformation($"SLIP ADD id={slip.SlipId} ticket={slip.TicketId}");
        }

        private void SetLimit(int limit)
        {
            Interlocked.Exchange(ref _limit, limit);
            while (_limit > 0 && Count() > _limit)
                Shift();
        }

        private void Shift() 
        {
            if (0 == Count())
                return;
            var oldest = Collection.FindAll().OrderBy(slip => slip.CreatedDate).FirstOrDefault();
            if (null != oldest)
                Remove(oldest.Id);
        }
    }
}
