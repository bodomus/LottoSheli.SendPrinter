using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity.Enums
{
    /// <summary>
    /// <see cref="LottoSheli.SendPrinter.Entity.IEntityCollectionObserver{TEntity}"/> Update state type
    /// </summary>
    public enum UpdateStateType
    {
        /// <summary>
        /// Create
        /// </summary>
        Create,
        /// <summary>
        /// Update
        /// </summary>
        Update,
        /// <summary>
        /// Remove
        /// </summary>
        Remove
    }
}
