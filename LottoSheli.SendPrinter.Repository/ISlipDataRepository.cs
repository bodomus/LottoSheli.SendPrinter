using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository
{
    public interface SlipDataEntityRepository : IBaseRepository<SlipDataEntity>
    {
        SlipDataEntity Get(int id);
        IEnumerable<SlipDataEntity> GetAll();
        SlipDataEntity Find(Func<SlipDataEntity, bool> predicate);
        IEnumerable<SlipDataEntity> FindAll(Func<SlipDataEntity, bool> predicate);
        
    }
}
