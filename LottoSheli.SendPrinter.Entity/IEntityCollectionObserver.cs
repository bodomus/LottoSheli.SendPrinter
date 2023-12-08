using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    /// <summary>
    /// Provides obeserver pattern for collections of <see cref="TEntity"/> updates.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityCollectionObserver<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Asyncronyously update the collections
        /// </summary>
        /// <param name="action"></param>
        /// <param name="affectedEnitites"></param>
        /// <returns></returns>
        Task UpdateAsync(NotifyCollectionChangedAction action, IEnumerable<TEntity> affectedEnitites);
    }
}
