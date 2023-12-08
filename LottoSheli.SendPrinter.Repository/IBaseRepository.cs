using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Repository
{
    /// <summary>
    /// A base interface for entity Repository
    /// </summary>
    public interface IBaseRepository
    {
        
    }

    /// <summary>
    /// Provides a basic CRUD and other commion methods for specified <see cref="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Specified entity type. Must be inherit from <see cref="BaseEntity"/>.</typeparam>
    public interface IBaseRepository<TEntity> : IBaseRepository where TEntity : BaseEntity
    {
        /// <summary>
        /// Creates a new instance of <see cref="TEntity"/> entity.
        /// </summary>
        /// <returns>A new instance of <see cref="TEntity"/> entity.</returns>
        TEntity CreateNew();

        /// <summary>
        /// Performs maintainance over specific storage implementation
        /// </summary>
        Task SynchronizeContext();

        /// <summary>
        /// Safe copy of db file to backups dolder
        /// </summary>
        Task BackupContext();

        /// <summary>
        /// Inserts specified <see cref="TEntity"/> entity to the storage. 
        /// </summary>
        /// <param name="entity">Specified <see cref="TEntity"/> entity.</param>
        /// <returns>Inserted <see cref="TEntity"/> entity.</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Updates specified <see cref="TEntity"/> entity in the storage. 
        /// </summary>
        /// <param name="entity">Specified <see cref="TEntity"/> entity.</param>
        /// <returns>Updated <see cref="TEntity"/> entity.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Removes <see cref="TEntity"/> entity by it's specified ID.
        /// </summary>
        /// <param name="id">specified ID</param>
        /// <returns>True if enitity has found.</returns>
        bool Remove(int id);

        /// <summary>
        /// Removes <see cref="TEntity"/> conforming supplied predicate
        /// </summary>
        /// <param name="predicate">predicate expression</param>
        /// <returns>True if enitity has found.</returns>
        bool Remove(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// Returns <see cref="TEntity"/> entity by it's specified ID.
        /// </summary>
        /// <param name="id">specified ID</param>
        /// <returns>null if enitity has not found.</returns>
        TEntity GetById(int id);

        /// <summary>
        /// Returns <see cref="TEntity"/> entities in storage.
        /// </summary>
        /// <returns>The <see cref="TEntity"/> entities in storage.</returns>
        IEnumerable<TEntity> GetAll(int skip = 0, int take = 0);

        /// <summary>
        /// Returns all <see cref="TEntity"/> entities in storage conforming supplied predicate.
        /// </summary>
        /// <returns>The <see cref="TEntity"/> entities in storage.</returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns a single entiry <see cref="TEntity"/> conforming supplied predicate.
        /// </summary>
        /// <returns>The <see cref="TEntity"/> entities in storage.</returns>
        TEntity FindOne(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Clears <see cref="TEntity"/> entities in storage.
        /// </summary>
        /// <returns>Items cleaned count.</returns>
        int Clear();

        /// <summary>
        /// Returns count of <see cref="TEntity"/> entities in storage.
        /// </summary>
        /// <returns>Count of <see cref="TEntity"/> entities in storage.</returns>
        int Count();

        /// <summary>
        /// Attach specifed collection observer for <see cref="TEntity"/> updates.
        /// </summary>
        /// <param name="observer"></param>
        void Attach(IEntityCollectionObserver<TEntity> observer);

        /// <summary>
        /// Detach specifed collection observer from <see cref="TEntity"/> updates.
        /// </summary>
        /// <param name="observer"></param>
        void Dettach(IEntityCollectionObserver<TEntity> observer);
    }
}
