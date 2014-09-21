using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum AuraFlag
    {
        None         = 0x0000,
        EffectIndex0 = 0x0001,
        EffectIndex1 = 0x0002,
        EffectIndex2 = 0x0004,
        NotCaster    = 0x0008,
        Positive     = 0x0010,
        Duration     = 0x0020,
        Scalable     = 0x0040,
        Negative     = 0x0080,
        Unk100       = 0x0100,
        Unk400       = 0x0400,
        Unk1000      = 0x1000,
        Unk4000      = 0x4000
    }

    [Flags]
    public enum AuraFlagMoP
    {
        None     = 0x0000,
        NoCaster = 0x0001,
        Positive = 0x0002,
        Duration = 0x0004,
        Scalable = 0x0008,
        Negative = 0x0010,
        Unk20    = 0x0020
    }
}
