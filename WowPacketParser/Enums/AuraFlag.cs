using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum AuraFlag
    {
        None = 0x00,
        EffectIndex0 = 0x01,
        EffectIndex1 = 0x02,
        EffectIndex2 = 0x04,
        NotCaster = 0x08,
        Positive = 0x10,
        Duration = 0x20,
        Unknown = 0x40,
        Negative = 0x80
    }
}
