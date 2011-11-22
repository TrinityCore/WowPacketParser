using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum UnknownFlags // Controls the GUID mask in bitstreaming
    {
        None = 0x000000,
        Byte4 = 0x000001,
        Byte0 = 0x000002,
        Byte1 = 0x000004,
        Byte2 = 0x000008, // If it does NOT have this flag, then it has field 6
        Unk5 = 0x000010,
        Byte3 = 0x000020,
        Byte7 = 0x000040,
        Byte5 = 0x000080,
        Unk9 = 0x000100,
        Unk10 = 0x000200,
        Unk11 = 0x000400,
        Unk12 = 0x000800,
        Unk13 = 0x001000,
        Unk14 = 0x002000,
        Unk15 = 0x004000,
        Unk16 = 0x008000,
        Unk17 = 0x010000,
        Unk18 = 0x020000,
        Unk19 = 0x040000,
        Unk20 = 0x080000,
    }
}
