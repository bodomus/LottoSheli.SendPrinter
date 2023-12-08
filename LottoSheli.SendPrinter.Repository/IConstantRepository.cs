using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Repository
{
    /// <summary>
    /// Provides storage access methods for specified <see cref="Constant"/> entity.
    /// </summary>
    /// <typeparam name="TEntity">Specified <see cref="Constant"/> entity.</typeparam>
    public interface IConstantRepository : IBaseRepository<Constant>
    {
        /// <summary>
        /// Get <see cref="Constant"/> by Name
        /// </summary>
        /// <param name="constantName"></param>
        /// <returns></returns>
        Constant GetByName(string constantName);
    }
}
