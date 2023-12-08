using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LottoSheli.SendPrinter.Entity
{
    /// <summary>
    /// Represents one table of a lottery ticket
    /// maps json layout file
    /// </summary>
    /// <typeparam name="T">Type of numbers (string or int)</typeparam>
    public class TicketTable
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("nums")]
        public IList<dynamic> Numbers { get; set; }

        [JsonProperty("strong")]
        public IList<dynamic> Strong { get; set; }

        //For json serializer
        protected TicketTable()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index"> index of table</param>
        /// <param name="numbers">numbers</param>
        public TicketTable(int index, IEnumerable<dynamic> numbers)
            : this(index, numbers, new List<dynamic>())
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index"> index of table</param>
        /// <param name="numbers">numbers</param>
        /// <param name="strong">strong number</param>
        public TicketTable(int index, IEnumerable<dynamic> numbers, object strong)
            : this(index, numbers, new List<dynamic> { strong })
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index"> index of table</param>
        public TicketTable(int index)
            : this(index, new List<dynamic>(), new List<dynamic>())
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index"> index of table</param>
        /// <param name="number">number</param>
        public TicketTable(int index, dynamic number)
            : this(index, new List<dynamic> { number }, new List<dynamic>())
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index"> index of table</param>
        /// <param name="numbers">numbers</param>
        /// <param name="strong">strong numbers</param>
        public TicketTable(int index, IEnumerable<dynamic> numbers, IEnumerable<dynamic> strong)
        {
            this.Index = index;
            this.Numbers = new List<dynamic>(numbers);
            this.Strong = new List<dynamic>(strong);
        }

        public override string ToString()
        {
            return String.Format("Table {0}: {1} [{2}]", Index, String.Join(", ", Numbers), String.Join(", ", Strong));
        }


        public override bool Equals(dynamic obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TicketTable)obj);
        }

        protected bool Equals(TicketTable other)
        {
            return Index == other.Index && Numbers.SequenceEqual(other.Numbers) && Strong.SequenceEqual(other.Strong);
        }

        public override int GetHashCode()
        {
            Debug.Assert(Numbers != null);
            Debug.Assert(Strong != null);

            unchecked
            {
                var hashCode = Index;
                foreach (var number in Numbers)
                {
                    hashCode = (hashCode * 397) ^ number.GetHashCode();
                }

                foreach (var i in Strong)
                {
                    hashCode = (hashCode * 397) ^ i.GetHashCode();
                }

                return hashCode;
            }
        }
    }
}
