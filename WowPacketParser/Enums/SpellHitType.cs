using System;

namespace WowPacketParser.Enums
{
    [Flags]
    enum SpellHitType
    {
        SPELL_HIT_TYPE_UNK1 = 0x00000001,
        SPELL_HIT_TYPE_CRIT = 0x00000002,
        SPELL_HIT_TYPE_UNK3 = 0x00000004,
        SPELL_HIT_TYPE_UNK4 = 0x00000008,
        SPELL_HIT_TYPE_UNK5 = 0x00000010,
        SPELL_HIT_TYPE_UNK6 = 0x00000020
    };
}
