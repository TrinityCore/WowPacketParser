using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GroupMemberStatusFlag
    {
        Offline      = 0x0000,
        Online       = 0x0001,
        Pvp          = 0x0002,
        Dead         = 0x0004,
        Ghost        = 0x0008,
        PvpFFA       = 0x0010,
        PvpInactive  = 0x0020, // HasAura 43681
        Afk          = 0x0040,
        Dnd          = 0x0080,
        Unknown9     = 0x0100,
        UsingVehicle = 0x0200,
        Unknown11    = 0x0400,
        Unknown12    = 0x0800
    }
}
