using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;

namespace LottoSheli.SendPrinter.Repository
{
    /// <summary>
    /// Provides storage access methods for specified <see cref="Draw"/> entity.
    /// </summary>
    /// <typeparam name="TEntity">Specified <see cref="Draw"/> entity.</typeparam>
    public interface IDrawRepository : IBaseRepository<Draw>
    {
        Draw GetByDrawGameType(DrawGameType gameType);
        void UpsertDraw(Draw draw);
    }
}
