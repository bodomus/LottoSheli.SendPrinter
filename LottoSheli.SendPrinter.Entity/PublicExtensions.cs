using LottoSheli.SendPrinter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    public static class PublicExtensions
    {
        public static bool IsMatchByNumbers(this TicketTable table1, TicketTable table2)
        {
            return EnumerableEquals(table1.Numbers, table2.Numbers) && EnumerableEquals(table1.Strong, table2.Strong);
        }


        /// <summary>
        /// checks if to enumerables of same type are equal
        /// </summary>
        /// <typeparam name="T">type of enumerable items</typeparam>
        /// <param name="list1">first enumerable</param>
        /// <param name="list2">second enumerable</param>
        /// <returns>true if enumerables are equal</returns>
        public static bool EnumerableEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

        /// <summary>
        /// Datermines when the task is in ordered type
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsOrderedTask(this TicketTask source)
        {
            return source.Type != Enums.TaskType.LottoRegular 
                && source.Type != Enums.TaskType.LottoDouble 
                && source.Type != Enums.TaskType.LottoSocial;
        }
    }
}
