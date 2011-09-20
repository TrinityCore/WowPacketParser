using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum CalendarFlag
    {
        None = 0x000,
        Unknown1 = 0x001,
        Unknown2 = 0x002,
        Unknown4 = 0x004,
        Unknown8 = 0x008,
        InvitesLocked = 0x010,
        WithoutInvites = 0x020,
        Unknown40 = 0x040,
        Unknown80 = 0x080,
        Unknown100 = 0x100,
        Unknown200 = 0x200,
        Unknown400 = 0x400,
        Unknown800 = 0x800
    }
}
