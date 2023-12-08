using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.DTO;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace LottoSheli.SendPrinter.Commands.OCR
{

    /// <summary>
    /// Subscribes to Enity collection state changes.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class SubscribeEntityStateCommand<TEntity, TRepository> : ISubscribeEntityStateCommand<TEntity> 
        where TEntity : BaseEntity
        where TRepository : IBaseRepository<TEntity>
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<TEntity> _subscribeRepository;

        protected SubscribeEntityStateCommand(ILogger logger, IBaseRepository<TEntity> subscribeRepository)
        {
            _logger = logger;

            _subscribeRepository = subscribeRepository;
        }


        public bool CanExecute()
        {
            return true;
        }

        public virtual EntityObservableCollection<TEntity> Execute(SubscribeEntityStateCommandData<TEntity> data)
        {
            var initList = data.FillWithActualState ? _subscribeRepository.GetAll() : Enumerable.Empty<TEntity>();
            
            var result = new EntityObservableCollection<TEntity>(initList, data.SortComparison, data.SafeUpdateStrategy);

            _subscribeRepository.Attach(result);

            return result;
        }
    }
}
