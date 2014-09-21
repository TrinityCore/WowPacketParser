using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ItemFlagExtra
    {
        None             = 0x00000000,
        HordeOnly        = 0x00000001,
        AllianceOnly     = 0x00000002,
        Refundable       = 0x00000004,
        Unknown1         = 0x00000008,
        Unknown2         = 0x00000010,
        Unknown3         = 0x00000020,
        Unknown4         = 0x00000040,
        Unknown5         = 0x00000080,
        NeedRollDisabled = 0x00000100,
        CasterWeapon     = 0x00000200,
        HasNormalPrice   = 0x00004000,
        BNetAccountBound = 0x00020000,
        CannotBeTransmog = 0x00200000,
        CannotTransmog   = 0x00400000,
        CanTransmog      = 0x00800000
    }
}
