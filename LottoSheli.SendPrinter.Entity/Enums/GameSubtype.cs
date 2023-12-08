using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Entity.Enums
{
    public enum GameSubtype
    {
        Unknown = -1,
        Regular = 0x0A00,
        Methodical8x1 = 0x0A01,
        Methodical9x1 = 0x0A02,
        Methodical10x1 = 0x0A03,
        Methodical11x1 = 0x0A04,
        Methodical12x1 = 0x0A05,
        Methodical5x1 = 0x0A06,
        Methodical7x4 = 0x0A07,
        Methodical7x5 = 0x0A08,
        Methodical7x6 = 0x0A09,
        Methodical7x7 = 0x0A0A,
        Chance1 = 0x0F00,
        Chance2 = 0x0F01,
        Chance3 = 0x0F02,
        Chance4 = 0x0F03,
        MultipleChance = 0x0F04,
        Regular7 = 0x1700,
        Methodical8 = 0x1701,
        Methodical9 = 0x1702,
        Regular123 = 0x1900
    }
}
