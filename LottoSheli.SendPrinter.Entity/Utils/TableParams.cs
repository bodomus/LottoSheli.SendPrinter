using LottoSheli.SendPrinter.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity.Utils
{
    public class TableParams
    {
        public int Take { get; set; }
        public int Min { get; set; } = 1;
        public int Max { get; set; }

        public static TableParams GetTableParams(GameType gt, GameSubtype st) 
        {
            return gt switch 
            { 
                GameType.Lotto => st switch 
                { 
                    GameSubtype.Regular => new LottoTableParams { Min = 1, Max = 37, Take = 6, StrongParams = new TableParams { Min = 1, Max = 7, Take = 1 } },
                    GameSubtype.Methodical7x4 => new LottoTableParams { Min = 1, Max = 37, Take = 7, StrongParams = new TableParams { Min = 1, Max = 7, Take = 4 } },
                    GameSubtype.Methodical7x5 => new LottoTableParams { Min = 1, Max = 37, Take = 7, StrongParams = new TableParams { Min = 1, Max = 7, Take = 5 } },
                    GameSubtype.Methodical7x6 => new LottoTableParams { Min = 1, Max = 37, Take = 7, StrongParams = new TableParams { Min = 1, Max = 7, Take = 6 } },
                    GameSubtype.Methodical7x7 => new LottoTableParams { Min = 1, Max = 37, Take = 7, StrongParams = new TableParams { Min = 1, Max = 7, Take = 7 } },
                    GameSubtype.Methodical5x1 => new LottoTableParams { Min = 1, Max = 37, Take = 5, StrongParams = new TableParams { Min = 1, Max = 7, Take = 1 } },
                    GameSubtype.Methodical8x1 => new LottoTableParams { Min = 1, Max = 37, Take = 8, StrongParams = new TableParams { Min = 1, Max = 7, Take = 1 } },
                    GameSubtype.Methodical9x1 => new LottoTableParams { Min = 1, Max = 37, Take = 9, StrongParams = new TableParams { Min = 1, Max = 7, Take = 1 } },
                    GameSubtype.Methodical10x1 => new LottoTableParams { Min = 1, Max = 37, Take = 10, StrongParams = new TableParams { Min = 1, Max = 7, Take = 1 } },
                    GameSubtype.Methodical11x1 => new LottoTableParams { Min = 1, Max = 37, Take = 11, StrongParams = new TableParams { Min = 1, Max = 7, Take = 1 } },
                    GameSubtype.Methodical12x1 => new LottoTableParams { Min = 1, Max = 37, Take = 12, StrongParams = new TableParams { Min = 1, Max = 7, Take = 1 } },
                    _ => throw new ArgumentException($"Game type mismatch: {gt} cannot have subtype {st}")
                },
                GameType.Chance => st switch 
                {
                    GameSubtype.Chance1 => new TableParams { Min = 0, Max = 7, Take = 3 },
                    GameSubtype.Chance2 => new TableParams { Min = 0, Max = 7, Take = 3 },
                    GameSubtype.Chance3 => new TableParams { Min = 0, Max = 7, Take = 3 },
                    GameSubtype.Chance4 => new TableParams { Min = 0, Max = 7, Take = 3 },
                    GameSubtype.MultipleChance => new TableParams { Min = 0, Max = 7, Take = 3 },
                    _ => throw new ArgumentException($"Game type mismatch: {gt} cannot have subtype {st}")
                },
                GameType.G777 => st switch 
                {
                    GameSubtype.Regular7 => new TableParams { Min = 1, Max = 70, Take = 7 },
                    GameSubtype.Methodical8 => new TableParams { Min = 1, Max = 70, Take = 8 },
                    GameSubtype.Methodical9 => new TableParams { Min = 1, Max = 70, Take = 9 },
                    _ => throw new ArgumentException($"Game type mismatch: {gt} cannot have subtype {st}")
                },
                GameType.G123 => new TableParams { Min = 0, Max = 9, Take = 3 },
                _ => throw new ArgumentException($"Unknown game type: {gt}")
            };
        }
    }

    public class LottoTableParams : TableParams 
    { 
        public TableParams StrongParams { get; set; }
    }
}
