using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.Repository.LiteDB
{

    /// <summary>
    /// A base LiteDB implementation class
    /// </summary>
    public abstract class BaseRepositoryLiteDB : IBaseRepository
    { }


    /// <summary>
    /// A base LiteDB implementation wich provides a basic CRUD functionality and other commion methods for specified <see cref="TEntity"/>. 
    /// </summary>
    /// <typeparam name="TEntity">Specified entity type. Must be inherit from <see cref="BaseEntity"/>.</typeparam>
    public abstract class BaseRepositoryLiteDB<TEntity> : BaseRepositoryLiteDB, IDisposable, IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private int _disposing = 0;
        protected IDBCreatorFactory _dbFactory;
        private ISet<IEntityCollectionObserver<TEntity>> _observers = new HashSet<IEntityCollectionObserver<TEntity>>();
        protected BaseRepositoryLiteDB(IDBCreatorFactory dBCreatorFactory)
        {
            _dbFactory = dBCreatorFactory;
        }

        public virtual DBType EntityStorageType { get; } = DBType.Scanqueue;

        /// <summary>
        /// A <see cref="ILiteDatabase"/> access context.
        /// </summary>
        protected ILiteDatabase Context => _dbFactory.GetDBContext(EntityStorageType);

        /// <summary>
        /// The entity storage name for <see cref="TEntity"/> LiteDB entity.
        /// </summary>
        protected abstract string EntityStorageName { get;}

        protected virtual ILiteCollection<TEntity> Collection => Context.GetCollection<TEntity>(EntityStorageName);

        public void Attach(IEntityCollectionObserver<TEntity> observer)
        {
            _observers.Add(observer);
        }

        /// <summary>
        /// Performs maintainance of repository asynchronously
        /// </summary>
        public virtual Task SynchronizeContext()
        {
            return Task.Run(() => _dbFactory.SyncDBContext(EntityStorageType));
        }

        /// <summary>
        /// Safe copy of db file to backups dolder
        /// </summary>
        public virtual Task BackupContext() => _dbFactory.BackupDBContextToFile(EntityStorageType);

        /// <summary>
        /// Clears <see cref="TEntity"/> entities in LiteDB storage.
        /// </summary>
        /// <returns>Items cleaned count.</returns>
        public virtual int Clear()
        {
            var items = Collection.FindAll().ToList();
            var result =  Collection.DeleteAll();
            if (items.Count > 0)
                NotifyObserversAsync(NotifyCollectionChangedAction.Remove, items);

            return result;
        }


        /// <summary>
        /// Returns count of <see cref="TEntity"/> entities in LiteDB storage.
        /// </summary>
        /// <returns>Count of <see cref="TEntity"/> entities in LiteDB storage.</returns>
        public virtual int Count()
        {
            return Collection.Count();
        }

        /// <summary>
        /// Creates a new instance of <see cref="TEntity"/> entity.
        /// </summary>
        /// <returns>A new instance of <see cref="TEntity"/> entity.</returns>
        public abstract TEntity CreateNew();

        public void Dettach(IEntityCollectionObserver<TEntity> observer)
        {
            _observers.Remove(observer);
        }

        public virtual IEnumerable<TEntity> GetAll(int skip = 0, int take = 0)
        {
            int count = Collection.Count();
            if (skip > 0 && take > 0 && (skip + take) <= count)
            {
                skip = Math.Max(skip, 0);
                take = Math.Min(count - skip, take);
                return Collection.FindAll().Skip(skip).Take(take);
            }
            return Collection.FindAll();
        }

        public virtual TEntity GetById(int id)
        {
            return Collection.FindOne(x => x.Id == id);
        }

        public virtual TEntity GetByGuid(string guid)
        {
            return Collection.FindOne(x => x.Guid == guid);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Find(predicate);
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.FindOne(predicate);
        }

        /// <summary>
        /// Inserts specified <see cref="TEntity"/> entity to the LiteDB storage. 
        /// </summary>
        /// <param name="entity">Specified <see cref="TEntity"/> entity.</param>
        /// <returns>Inserted <see cref="TEntity"/> entity.</returns>
        public virtual TEntity Insert(TEntity entity)
        {
            var storedEntity = GetById(entity.Id);

            if (null != storedEntity)
                Collection.Delete(storedEntity.Id);

            Collection.Insert(entity);
            NotifyObserversAsync(NotifyCollectionChangedAction.Add, Enumerable.Repeat(entity, 1));

            return entity;
        }


        /// <summary>
        /// Removes <see cref="TEntity"/> entity by it's specified ID.
        /// </summary>
        /// <param name="id">specified ID</param>
        /// <returns>True if enitity has found.</returns>
        public virtual bool Remove(int id)
        {
            var item = GetById(id);
            var result = Collection.Delete(id);

            if (item != null)
                NotifyObserversAsync(NotifyCollectionChangedAction.Remove, Enumerable.Repeat(item, 1));

            return result;
        }

        /// <summary>
        /// Removes <see cref="TEntity"/> entity by it's specified ID.
        /// </summary>
        /// <param name="predicate">predicate deciding wheather remove an entity</param>
        /// <returns>True if enitity has found.</returns>
        public virtual bool Remove(Expression<Func<TEntity, bool>> predicate)
        {
            var items = Collection.Find(predicate);
            var deletionCount = Collection.DeleteMany(predicate);

            NotifyObserversAsync(NotifyCollectionChangedAction.Remove, items);
            return deletionCount > 0;
        }


        /// <summary>
        /// Updates specified <see cref="TEntity"/> entity in the LiteDB storage. 
        /// </summary>
        /// <param name="entity">Specified <see cref="TEntity"/> entity.</param>
        /// <returns>Updated <see cref="TEntity"/> entity.</returns>
        public virtual TEntity Update(TEntity entity)
        {
            Collection.Update(entity);

            NotifyObserversAsync(NotifyCollectionChangedAction.Move, Enumerable.Repeat(entity, 1));

            return entity;
        }

        public virtual void Backup()
        { }
        private Task NotifyObserversAsync(NotifyCollectionChangedAction action, IEnumerable<TEntity> affectedEnitites)
        {
            return Task.Run(() =>
            {
                foreach (var observer in _observers)
                    observer.UpdateAsync(action, affectedEnitites);
            });
        }

        public void Dispose()
        {
            if (_disposing > 0)
                return;
            Interlocked.Exchange(ref _disposing, 1);
            try
            {
                Context?.Commit();
                Context?.Checkpoint();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
