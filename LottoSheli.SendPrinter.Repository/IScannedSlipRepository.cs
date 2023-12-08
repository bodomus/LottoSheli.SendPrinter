using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository
{
    public interface IScannedSlipRepository : IBaseRepository<ScannedSlip>
    {
        int Limit { get; set; }
        void Upsert(ScannedSlip slip);
        bool HasSlip(string slipId);
        ScannedSlip GetSlip(string slipId);
    }
}
