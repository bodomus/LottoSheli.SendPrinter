using LottoSheli.SendPrinter.Commands.Base;
using LottoSheli.SendPrinter.Entity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;

namespace LottoSheli.SendPrinter.Commands.OCR
{
    /// <summary>
    /// The data for <see cref="ISubscribeEntityStateCommand"/>
    /// </summary>
    public class SubscribeEntityStateCommandData<TEntity> : ICommandData where TEntity : BaseEntity
    {
        public bool FillWithActualState { get; init; }

        public Action<NotifyCollectionChangedAction, IEnumerable<TEntity>, Action<NotifyCollectionChangedAction, IEnumerable<TEntity>>> SafeUpdateStrategy { get; init; }

        public Comparison<TEntity> SortComparison { get; init; }
    }
}
