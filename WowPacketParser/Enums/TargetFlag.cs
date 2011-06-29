using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum TargetFlag
    {
        Self = 0x00000000,
        SpellDynamic1 = 0x00000001,
        Unit = 0x00000002,
        SpellDynamic2 = 0x00000004,
        SpellDynamic3 = 0x00000008,
        Item = 0x00000010,
        SourceLocation = 0x00000020,
        DestinationLocation = 0x00000040,
        ObjectSelfTarget = 0x00000080,
        UnitSelfTarget = 0x00000100,
        PvpCorpse = 0x00000200,
        UnitCorpse = 0x00000400,
        Object = 0x00000800,
        TradeItem = 0x00001000,
        NameString = 0x00002000,
        OpenLock = 0x00004000,
        Corpse = 0x00008000,
        SpellDynamic4 = 0x00010000,
        Glyph = 0x00020000,
        Unknown4 = 0x00040000,
        ExtraTargets = 0x00080000 // 4.x VisualChain
    }
}
