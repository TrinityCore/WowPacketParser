using System;

namespace WowPacketParser.Enums
{
    [Flags]
    enum BattlegroundUpdateFlags
    {
        Unk01       = 0x01,
        Unk02       = 0x02,
        Unk04       = 0x04,
        Unk08       = 0x08,
        Unk10       = 0x10,
        Finished    = 0x20,
        ArenaScores = 0x40,
        ArenaNames  = 0x80
    }
}
