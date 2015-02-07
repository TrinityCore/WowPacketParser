using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum CustomizationFlag
    {
        None      = 0x00000000,
        Customize = 0x00000001,
        Unknown1  = 0x00000002,
        Unknown2  = 0x00000004,
        Unknown3  = 0x00000008,
        Unknown4  = 0x00000010,
        Unknown5  = 0x00000020,
        Unknown6  = 0x00000040,
        Unknown7  = 0x00000080,
        Unknown8  = 0x00000100,
        Unknown9  = 0x00000200,
        Unknown10 = 0x00000400,
        Unknown20 = 0x00000800,
        Unknown21 = 0x00001000,
        Unknown22 = 0x00002000,
        Unknown23 = 0x00004000,
        Unknown24 = 0x00008000,
        Faction   = 0x00010000,
        Unknown25 = 0x00020000,
        Unknown26 = 0x00040000,
        Unknown27 = 0x00080000,
        Race      = 0x00100000,
        Unknown28 = 0x00200000,
        Unknown29 = 0x00400000,
        Unknown30 = 0x00800000,
        Unknown31 = 0x01000000,
        Unknown32 = 0x02000000,
        Unknown33 = 0x04000000,
        Unknown34 = 0x08000000,
        Unknown35 = 0x10000000
    }
}
