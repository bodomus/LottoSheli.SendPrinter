using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.DTO;
using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Repository;
using System.Collections.Generic;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.OCR
{

    /// <summary>
    /// Subscribes to Enity collection state changes.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISubscribeEntityStateCommand<TEntity> : IParametrizedWithResultCommand<SubscribeEntityStateCommandData<TEntity>, EntityObservableCollection<TEntity>> 
        where TEntity : BaseEntity
    {

    }
}
