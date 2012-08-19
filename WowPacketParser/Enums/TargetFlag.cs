using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum TargetFlag
    {
        Self                = 0x00000000,
        SpellDynamic1       = 0x00000001,
        Unit                = 0x00000002,
        UnitRaid            = 0x00000004,
        UnitParty           = 0x00000008,
        Item                = 0x00000010,
        SourceLocation      = 0x00000020,
        DestinationLocation = 0x00000040,
        UnitEnemy           = 0x00000080,
        UnitAlly            = 0x00000100,
        CorpseEnemy         = 0x00000200,
        UnitDead            = 0x00000400,
        GameObject          = 0x00000800,
        TradeItem           = 0x00001000,
        NameString          = 0x00002000,
        GameObjectItem      = 0x00004000,
        CorpseAlly          = 0x00008000,
        UnitMinipet         = 0x00010000,
        Glyph               = 0x00020000,
        DestinationTarget   = 0x00040000,
        ExtraTargets        = 0x00080000, // 4.x VisualChain
        UnitPassenger       = 0x00100000,
        Unk400000           = 0x00400000,
        Unk1000000          = 0x01000000,
        Unk4000000          = 0x04000000,
        Unk10000000         = 0x10000000,
        Unk40000000         = 0x40000000
    }
}
