using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository
{
    /// <summary>
    /// Factory for <see cref="IBaseRepository"/> repositories
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets specified repository obejct
        /// </summary>
        /// <typeparam name="TRepo"></typeparam>
        /// <returns></returns>
        TRepo GetRepository<TRepo>() where TRepo : IBaseRepository;
    }
}
