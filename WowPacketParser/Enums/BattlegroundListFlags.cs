using System;

namespace PacketParser.Enums
{
    [Flags]
    enum BattlegroundListFlags
    {
        AlreadyWon     = 0x10,
        Unk20          = 0x20,
        NoBattlemaster = 0x80,
    }
}
