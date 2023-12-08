using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository
{
    /// <summary>
    /// Provides basic storage access methods for specified <see cref="TEntity"/> entity.
    /// </summary>
    /// <typeparam name="TEntity">Specified <see cref="TEntity"/> entity. Class must be inherited from <see cref="TicketTask"/></typeparam>
    public interface ITicketTaskRepositoryBase<TEntity> : IBaseRepository<TEntity> where TEntity : TicketTask
    {
        /// <summary>
        /// Returns true if specified <see cref="TEntity"/> storage already contains entity with spesified TicketId
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns>true if specified <see cref="TEntity"/> storage already contains entity with spesified TicketId</returns>
        bool ContainsTicketId(int ticketId);

        /// <summary>
        /// Returns next sequence number for the ticket <see cref="TEntity"/>
        /// </summary>
        /// <returns>sequence to use with ticket</returns>
        int GetNextSequence();

        /// <summary>
        /// Returns max sequence number among stored tickets <see cref="TEntity"/>
        /// </summary>
        /// <returns>sequence to use with ticket</returns>
        int GetMaxSequence();

        /// <summary>
        /// Returns first element by specified sequence number or null if sequence doesn't exists.
        /// </summary>
        /// <param name="sequenceNumber">specified sequence number</param>
        /// <returns>first element by specified sequence number or null if sequence doesn't exists.</returns>
        TEntity GetBySequence(int sequenceNumber);

        /// <summary>
        /// Returns first element by specified sequences ranage
        /// </summary>
        /// <param name="startSequenceNumber">start specified sequence number</param>
        /// /// <param name="endSequenceNumber">end specified sequence number</param>
        /// <returns>first element by specified sequences ranage.</returns>
        IEnumerable<TEntity> GetBetweenSequences(int startSequenceNumber, int endSequenceNumber);

        /// <summary>
        /// Finds the ticket by specified index.
        /// </summary>
        /// <param name="userId">Specified user id. Can be null.</param>
        /// <param name="tables">Specified tables. Cannot be null.</param>
        /// <returns>null if ticket is not found</returns>
        IEnumerable<TEntity> FindByIndex(string userId, IEnumerable<TicketTable> tables);


        /// <summary>
        /// Finds the ticket by specified index.
        /// </summary>
        /// <param name="nationalId">Specified national id. Can be null. In decimal format.</param>
        /// <param name="tables">Specified tables. Cannot be null.</param>
        /// <returns>null if ticket is not found</returns>
        IEnumerable<TEntity> FindByIndex(IEnumerable<TaskType> taskTypes, string nationalId, IEnumerable<TicketTable> tables);

        /// <summary>
        /// Finds the ticket by tcket id.
        /// </summary>
        /// <param name="ticketId">Specified ticket id. Cannot be null.</param>
        /// <returns>null if ticket is not found</returns>
        TEntity FindByTicketId(int ticketId);

        /// <summary>
        /// Finds the ticket by sequence number.
        /// </summary>
        /// <param name="sequenceNumber">Specified sequence number.</param>
        /// <returns>null if ticket is not found</returns>
        TEntity FindBySequence(int sequenceNumber);


        /// <summary>
        /// Finds ticket collection by specified <see cref="TicketTable"/>
        /// </summary>
        /// <param name="tableRow">specified <see cref="TicketTable"/></param>
        /// <returns>ticket collection</returns>
        IEnumerable<TEntity> FindByTicketTableRow(TicketTable tableRow);


        /// <summary>
        /// Finds ticket collection by specified <see cref="TaskType"/>
        /// </summary>
        /// <param name="taskType">specified <see cref="TaskType"/></param>
        /// <returns>ticket collection</returns>
        IEnumerable<TEntity> FindByType(TaskType taskType);
    }
}
