using LiteDB;
using LottoSheli.SendPrinter.Core;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{
    /// <summary>
    /// Provides basic LiteDB storage access methods for specified <see cref="TEntity"/> entity.
    /// </summary>
    /// <typeparam name="TEntity">Specified <see cref="TEntity"/> entity. Class must be inherited from <see cref="TicketTask"/></typeparam>
    public abstract class TicketTaskRepositoryBaseLiteDB<TEntity> : BaseRepositoryLiteDB<TEntity>, ITicketTaskRepositoryBase<TEntity> where TEntity : TicketTask
    {
        private readonly IHashGenerator _hashGenerator;
        private readonly ILogger _logger;

        private int _sequence = -1;

        protected TicketTaskRepositoryBaseLiteDB(IDBCreatorFactory dBCreatorFactory, IHashGenerator hashGenerator, ILogger logger) : base(dBCreatorFactory)
        {
            _hashGenerator = hashGenerator;
            _sequence = GetMaxSequence();
        }

        /// <summary>
        /// Returns true if specified <see cref="TEntity"/> storage already contains entity with spesified TicketId
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public virtual bool ContainsTicketId(int ticketId)
        {
            return Collection.Exists(obj => obj.TicketId == ticketId);
        }

        /// <summary>
        /// Returns first element by specified sequence number or null if sequence doesn't exists.
        /// </summary>
        /// <param name="sequenceNumber">specified sequence number</param>
        /// <returns>first element by specified sequence number or null if sequence doesn't exists.</returns>
        public TEntity GetBySequence(int sequenceNumber)
        {
            return Collection.FindOne(obj => obj.Sequence == sequenceNumber);
        }

        /// <summary>
        /// Thread safe self-incrementing getter for assigning Sequence to TicketTask <see cref="TEntity"/>
        /// </summary>
        /// <returns>Sequence to use on TicketTask<see cref="TEntity"/></returns>
        public virtual int GetNextSequence()
        {
            return GetMaxSequence() + 1;
        }

        public int GetMaxSequence()
        {
            return Collection.Count() > 0 ? Collection.Max(obj => obj.Sequence) : -1;
        }

        /// <summary>
        /// Returns first element by specified sequences ranage
        /// </summary>
        /// <param name="startSequenceNumber">start specified sequence number</param>
        /// /// <param name="endSequenceNumber">end specified sequence number</param>
        /// <returns>first element by specified sequences ranage.</returns>
        public virtual IEnumerable<TEntity> GetBetweenSequences(int startSequenceNumber, int endSequenceNumber)
        {
            var col = Context.GetCollection<TEntity>(EntityStorageName);

            return col.Query().Where(obj => obj.Sequence >= startSequenceNumber && obj.Sequence <= endSequenceNumber).ToEnumerable();
        }


        /// <summary>
        /// Finds the ticket by specified index.
        /// </summary>
        /// <param name="nationalId">Specified national id. Can be null. In decimal format.</param>
        /// <param name="tables">Specified tables. Cannot be null.</param>
        /// <returns>null if ticket is not found</returns>
        public virtual IEnumerable<TEntity> FindByIndex(string userId, IEnumerable<TicketTable> tables)
        {
            var col = Context.GetCollection<TEntity>(EntityStorageName);

            if (!string.IsNullOrEmpty(userId))
            {
                var tablesHashWithUser = $"{userId}:{_hashGenerator.ComputeHash(string.Join(string.Empty, tables.Select(obj => obj.ToString())))}";
                return col.Find(obj => obj.TablesHash == tablesHashWithUser);
            }

            var stringToHash = string.Join(string.Empty, tables.Select(obj => obj.ToString()));
            var tablesHash = $":{_hashGenerator.ComputeHash(stringToHash)}";

            //_logger.LogInformation($"stringtoHash: {stringToHash} computedHash: {tablesHash}");

            return col.Find(obj => obj.TablesHash.EndsWith(tablesHash)).ToList();
        }


        /// <inheritdoc/>
        public virtual IEnumerable<TEntity> FindByIndex(IEnumerable<TaskType> taskTypes, string nationalId, IEnumerable<TicketTable> tables)
        {
            var col = Context.GetCollection<TEntity>(EntityStorageName);

            var tablesHash = default(string);

            if (!string.IsNullOrEmpty(nationalId))
            {
                tablesHash = $"{nationalId}:{_hashGenerator.ComputeHash(string.Join(string.Empty, tables.Select(obj => obj.ToString())))}";
            }
            else
            {
                var stringToHash = string.Join(string.Empty, tables.Select(obj => obj.ToString()));
                tablesHash = $":{_hashGenerator.ComputeHash(stringToHash)}";
            }

            var query = col.Query().Where(obj => taskTypes.Contains(obj.Type) && obj.TablesHash.EndsWith(tablesHash));

            return query.ToArray();
        }

        public override IEnumerable<TEntity> GetAll(int skip = 0, int take = 0) => 
            base.GetAll(skip, take).OrderBy(obj => obj.Sequence);

        public TEntity FindByTicketId(int ticketId) => 
            Collection.FindOne(obj => obj.TicketId == ticketId);

        public TEntity FindBySequence(int sequenceNumber) => 
            Collection.FindOne(obj => obj.Sequence == sequenceNumber);

        public IEnumerable<TEntity> FindByTicketTableRow(TicketTable tableRow) => 
            Collection.Find(obj => obj.Tables.Any(tbl => tbl.IsMatchByNumbers(tableRow)));

        public IEnumerable<TEntity> FindByType(TaskType taskType) => 
            Collection.Find(t => t.Type == taskType);
    }
}
