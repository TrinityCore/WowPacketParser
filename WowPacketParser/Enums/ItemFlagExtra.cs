using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ItemFlagExtra
    {
        None = 0x0000,
        HordeOnly = 0x0001,
        AllianceOnly = 0x0002,
        Refundable = 0x0004,
        Unknown1 = 0x0008,
        Unknown2 = 0x0010,
        Unknown3 = 0x0020,
        Unknown4 = 0x0040,
        Unknown5 = 0x0080,
        NeedRollDisabled = 0x0100
    }
}
