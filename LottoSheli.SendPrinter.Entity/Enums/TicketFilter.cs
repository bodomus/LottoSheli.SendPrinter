using System;

namespace LottoSheli.SendPrinter.Entity.Enums
{
    [Flags]
    public enum TicketFilter
    {
        Undefined   = 0,
        Lotto       = 0x01,
        Chance      = 0x02,
        OneTwoThree = 0x04,
        SevenSevenSeven = 0x08,
        // Subtypes
        Regular     = 0x10,
        Methodical  = 0x20,
        Strong      = 0x40,
        Double      = 0x80,
        Multiple    = 0x100
    }
}
