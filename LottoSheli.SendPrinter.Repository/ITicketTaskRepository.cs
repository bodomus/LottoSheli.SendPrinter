using LottoSheli.SendPrinter.Entity;
using System.Collections.Generic;

namespace LottoSheli.SendPrinter.Repository
{
    /// <summary>
    /// Provides storage access methods for specified <see cref="TicketTask"/> entity.
    /// </summary>
    /// <typeparam name="TEntity">Specified <see cref="TicketTask"/> entity.</typeparam>
    public interface ITicketTaskRepository : ITicketTaskRepositoryBase<TicketTask>
    {

    }
}
