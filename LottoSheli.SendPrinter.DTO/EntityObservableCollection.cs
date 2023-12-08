using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.DTO
{
    /// <summary>
    /// Provides Observable collection for specified <see cref="TEntity"/>
    /// </summary>
    /// <typeparam name="TEntity">Specified Enityt Type</typeparam>
    /// <remarks>Winforms doesn't supports for <see cref="ObservableCollection{TEntity}"/> and we use <see cref="BindingList{TEntity}"/> instead.</remarks>
    public class EntityObservableCollection<TEntity> : BindingList<TEntity>, IEntityCollectionObserver<TEntity> where TEntity : BaseEntity
    {

        public Action<NotifyCollectionChangedAction, IEnumerable<TEntity>, Action<NotifyCollectionChangedAction, IEnumerable<TEntity>>> _safeUpdateStrategy;

        public Comparison<TEntity> _sortComparison = null;

        public EntityObservableCollection()
        {
            
        }

        public EntityObservableCollection(IEnumerable<TEntity> initList, Comparison<TEntity> sortComparison = null, Action<NotifyCollectionChangedAction, IEnumerable<TEntity>, Action<NotifyCollectionChangedAction, IEnumerable<TEntity>>> safeUpdateStrategy = null) 
            :base(initList.ToList())
        {
            _sortComparison = sortComparison;
            _safeUpdateStrategy = safeUpdateStrategy;
        }


        private void UpdateInvoke(NotifyCollectionChangedAction action, IEnumerable<TEntity> affectedEnitites)
        {
            switch (action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in affectedEnitites)
                        Add(item);
                    break;
                case NotifyCollectionChangedAction.Move:
                    foreach (var item in affectedEnitites)
                    {
                        var idx = IndexOf(item);

                        if (idx < 0)
                            Add(item);
                        else
                            this[idx] = item;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in affectedEnitites)
                        Remove(item);
                    break;
                default:
                    throw new NotSupportedException(action.ToString());
            }
        }

        public Task UpdateAsync(NotifyCollectionChangedAction action, IEnumerable<TEntity> affectedEnitites)
        {
            return Task.Run(() =>
            {
                if (_safeUpdateStrategy != null)
                    _safeUpdateStrategy(action, affectedEnitites, UpdateInvoke);
                else
                    UpdateInvoke(action, affectedEnitites);
            });
        }
    }
}
