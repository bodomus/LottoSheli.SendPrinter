using System;

namespace LottoSheli.SendPrinter.Entity
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Entity unique ID
        /// </summary>
        public abstract int Id { get; set; }

        /// <summary>
        /// Entity created date
        /// </summary>
        public abstract DateTime CreatedDate { get; set; }

        public override bool Equals(object obj)
        {
            var result = obj as BaseEntity;
            return result?.Id ==  Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public virtual string Guid { get; set; }
    }
}
