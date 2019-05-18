using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum UpdateFieldFlag : byte
    {
        None = 0,
        Owner = 0x01,
        PartyMember = 0x02,
        UnitAll = 0x04,
        Empath = 0x08
    }
}
