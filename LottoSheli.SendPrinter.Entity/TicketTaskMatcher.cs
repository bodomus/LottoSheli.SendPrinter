using LottoSheli.SendPrinter.Entity;
using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity
{
    public class TicketTaskMatcher
    {
        private static readonly Dictionary<TaskType, GameType> _typesMap = new Dictionary<TaskType, GameType> 
        {
            {TaskType.LottoRegular, GameType.Lotto },
            {TaskType.LottoSocial, GameType.Lotto },
            {TaskType.LottoDouble, GameType.Lotto },
            {TaskType.TripleSeven, GameType.G777 },
            {TaskType.Combined123, GameType.G123 },
            {TaskType.Regular123, GameType.G123 },
            {TaskType.RegularChance, GameType.Chance },
            {TaskType.MethodicalChance, GameType.Chance },
            {TaskType.MultipleChance, GameType.Chance },
        };
        private static readonly List<(TaskSubType, GameSubtype)> _subsMap = new List<(TaskSubType, GameSubtype)>
        {
            new ( TaskSubType.LottoRegular, GameSubtype.Regular ),
            new ( TaskSubType.LottoDouble, GameSubtype.Regular ),
            new ( TaskSubType.LottoSocial, GameSubtype.Regular ),
            new ( TaskSubType.LottoMethodical, GameSubtype.Methodical8x1 ),
            new ( TaskSubType.LottoMethodical, GameSubtype.Methodical9x1 ),
            new ( TaskSubType.LottoMethodical, GameSubtype.Methodical10x1 ),
            new ( TaskSubType.LottoMethodical, GameSubtype.Methodical11x1 ),
            new ( TaskSubType.LottoMethodical, GameSubtype.Methodical12x1 ),
            new ( TaskSubType.LottoMethodical, GameSubtype.Methodical5x1 ),
            new ( TaskSubType.LottoDoubleMethodical, GameSubtype.Methodical8x1 ),
            new ( TaskSubType.LottoDoubleMethodical, GameSubtype.Methodical9x1 ),
            new ( TaskSubType.LottoDoubleMethodical, GameSubtype.Methodical10x1 ),
            new ( TaskSubType.LottoDoubleMethodical, GameSubtype.Methodical11x1 ),
            new ( TaskSubType.LottoDoubleMethodical, GameSubtype.Methodical12x1 ),
            new ( TaskSubType.LottoDoubleMethodical, GameSubtype.Methodical5x1 ),
            new ( TaskSubType.LottoStrongMethodical, GameSubtype.Methodical7x4 ),
            new ( TaskSubType.LottoStrongMethodical, GameSubtype.Methodical7x5 ),
            new ( TaskSubType.LottoStrongMethodical, GameSubtype.Methodical7x6 ),
            new ( TaskSubType.LottoStrongMethodical, GameSubtype.Methodical7x7 ),
            new ( TaskSubType.LottoDoubleStrongMethodical, GameSubtype.Methodical7x4 ),
            new ( TaskSubType.LottoDoubleStrongMethodical, GameSubtype.Methodical7x5 ),
            new ( TaskSubType.LottoDoubleStrongMethodical, GameSubtype.Methodical7x6 ),
            new ( TaskSubType.LottoDoubleStrongMethodical, GameSubtype.Methodical7x7 ),
            new ( TaskSubType.TripleSeven, GameSubtype.Regular7 ),
            new ( TaskSubType.TripleSevenMethodical, GameSubtype.Methodical8 ),
            new ( TaskSubType.TripleSevenMethodical, GameSubtype.Methodical9 ),
            new ( TaskSubType.RegularChance, GameSubtype.Chance1 ),
            new ( TaskSubType.RegularChance, GameSubtype.Chance2 ),
            new ( TaskSubType.RegularChance, GameSubtype.Chance3 ),
            new ( TaskSubType.RegularChance, GameSubtype.Chance4 ),
            new ( TaskSubType.MultipleChance, GameSubtype.MultipleChance ),
            new ( TaskSubType.MethodicalChance, GameSubtype.Chance1 ),
            new ( TaskSubType.MethodicalChance, GameSubtype.Chance2 ),
            new ( TaskSubType.MethodicalChance, GameSubtype.Chance3 ),
            new ( TaskSubType.MethodicalChance, GameSubtype.Chance4 ),
            new ( TaskSubType.MethodicalChance, GameSubtype.MultipleChance ),
            new ( TaskSubType.The123, GameSubtype.Regular123 ),
        };
        
        private static readonly HashSet<TaskSubType> _doubleSubtypes = new HashSet<TaskSubType> 
        {
            TaskSubType.LottoDouble,
            TaskSubType.LottoDoubleMethodical,
            TaskSubType.LottoDoubleStrongMethodical
        };

        private SlipDataEntity _data;
        private TicketTask _ticket;
        private List<Func<SlipDataEntity, TicketTask, bool>> _matchers;

        public bool IsMatch => _matchers.All(matcher => matcher(_data, _ticket));

        public static IEnumerable<(TaskType, GameType)> Types => _typesMap.AsEnumerable()
            .Select<KeyValuePair<TaskType, GameType>, (TaskType, GameType)>((kv) => new (kv.Key, kv.Value));

        public static IEnumerable<(TaskSubType, GameSubtype)> SubTypes => _subsMap.AsEnumerable();

        public static IEnumerable<TaskSubType> DoubleSubTypes => _doubleSubtypes.AsEnumerable();

        public TicketTaskMatcher(SlipDataEntity slipData, TicketTask ticketTask) 
        { 
            _data = slipData;
            _ticket = ticketTask;
            _matchers = new List<Func<SlipDataEntity, TicketTask, bool>>();
            Initialize();
        }

        public void Initialize(params Func<SlipDataEntity, TicketTask, bool>[] matchers) 
        {
            _matchers.Clear();
            if (0 == matchers.Length)
            {
                _matchers.Add(MatchType);
                _matchers.Add(MatchSubtype);
                _matchers.Add(MatchPrice);
                _matchers.Add(MatchTables);
                _matchers.Add(MatchNationalId);
            }
            else 
            {
                _matchers.AddRange(matchers);
            }
        }

        public static bool MatchType(SlipDataEntity slipData, TicketTask ticketTask) 
        {
            if (_typesMap.TryGetValue(ticketTask.Type, out GameType gt))
                return slipData.GameType == gt;
            return false;
        }

        public static bool MatchSubtype(SlipDataEntity slipData, TicketTask ticketTask) 
        {
            var matchingSubtypes = _subsMap.Where(tuple => 
                tuple.Item1 == ticketTask.SubType 
                && tuple.Item2 == slipData.GameSubtype 
                && slipData.IsDouble == _doubleSubtypes.Contains(ticketTask.SubType)).ToList();

            return matchingSubtypes.Count > 0;
        }

        public static bool MatchPrice(SlipDataEntity slipData, TicketTask ticketTask) 
        {
            return (int)ticketTask.Price == slipData.Price;
        }

        public static bool MatchTables(SlipDataEntity slipData, TicketTask ticketTask)
        {
            var sdTables = slipData.Tables.ToList();
            var ttTables = ticketTask.Tables.ToList();

            for (int i = 0; i < sdTables.Count; i++)
            { 
                var sdTab = sdTables[i];
                var ttTab = ttTables.Find(t => t.Index == i) ?? new TicketTable(i);

                if (!MatchTablePair(slipData.GameType, sdTab, ttTab))
                    return false;
            }
            return true;
        }

        public static bool MatchNationalId(SlipDataEntity slipData, TicketTask ticketTask) 
        {
            if (slipData.ShowNationalId != (ticketTask.UserIdMandatoryFlag > 0))
                return false;

            if (slipData.ShowNationalId)
                return !string.IsNullOrEmpty(slipData.NationalId) && slipData.UserId.Equals(ticketTask.UserId);
            
            return true;
        }

        private static bool MatchTablePair(GameType gtype, TicketTable fromData, TicketTable fromTask) 
        { 
            if (gtype == GameType.Lotto)
                 return MatchLottoTables(fromData, fromTask);
            if (gtype == GameType.G777)
                return Match777Tables(fromData, fromTask);
            if (gtype == GameType.G123)
                return Match123Tables(fromData, fromTask);
            if (gtype == GameType.Chance)
                return MatchChanceTables(fromData, fromTask);
            return false;
        }

        private static bool MatchLottoTables(TicketTable fromData, TicketTable fromTask) => 
            MatchUnorderedCollection(fromData.Numbers, fromTask.Numbers) 
            && MatchUnorderedCollection(fromData.Strong, fromTask.Strong);

        private static bool Match777Tables(TicketTable fromData, TicketTable fromTask) =>
            MatchUnorderedCollection(fromData.Numbers, fromTask.Numbers);

        private static bool Match123Tables(TicketTable fromData, TicketTable fromTask) =>
            MatchOrderedCollection(fromData.Numbers, fromTask.Numbers);

        private static bool MatchChanceTables(TicketTable fromData, TicketTable fromTask) =>
            MatchOrderedCollection(fromData.Numbers.Select(x => x.ToString("R")), fromTask.Numbers);

        private static bool MatchUnorderedCollection(IEnumerable<object> colA, IEnumerable<object> colB) 
        {
            if (null == colA || null == colB) return false;

            var acolA = new HashSet<string>(colA.Select(x => x.ToString()));
            var acolB = new HashSet<string>(colB.Select(x => x.ToString()));
            
            return acolA.SetEquals(acolB);
        }

        private static bool MatchOrderedCollection(IEnumerable<object> colA, IEnumerable<object> colB)
        {
            if (null == colA || null == colB) return false;

            var acolA = colA.Select(x => x.ToString()).ToList();
            var acolB = colB.Select(x => x.ToString()).ToList();
            return acolA.SequenceEqual(acolB);
        }

        public static TicketMatchingError Match(SlipDataEntity slipData, TicketTask ticketTask)
        {
            // Order is important here. It resembles "severity" of each mismatch type
            // This allow finding tickets with imperfect match
            if (!MatchType(slipData, ticketTask))
                return TicketMatchingError.Type;

            if (!MatchSubtype(slipData, ticketTask))
                return TicketMatchingError.Subtype;

            if (!MatchTables(slipData, ticketTask))
                return TicketMatchingError.Tables;

            if (!MatchPrice(slipData, ticketTask))
                return TicketMatchingError.Price;

            if (!MatchNationalId(slipData, ticketTask))
                return TicketMatchingError.NationalId;

            return TicketMatchingError.None;
        }

        public static IEnumerable<TaskType> GetMatchingTaskTypes(GameType gameType) 
        { 
            foreach(var kv in _typesMap)
                if (kv.Value == gameType)
                    yield return kv.Key;
        }

        public static IEnumerable<TaskSubType> GetMatchingTaskSubTypes(GameSubtype subType)
        {
            foreach (var kv in _subsMap)
                if (kv.Item2 == subType)
                    yield return kv.Item1;
        }

        public static GameType GetMatchingGameType(TaskType taskType)
        {
            return _typesMap[taskType];
        }

        public static IEnumerable<GameSubtype> GetMatchingGameSubTypes(TaskSubType subtype)
        {
            foreach (var kv in _subsMap)
                if (kv.Item1 == subtype)
                    yield return kv.Item2;
        }

        private static int CalculateCombinedSubtype(int subType, bool isDouble) => (subType << 1) + (isDouble ? 1 : 0);

    }
}
