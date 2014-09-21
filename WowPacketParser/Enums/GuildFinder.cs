using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GuildFinderOptionsInterest
    {
        Questing    = 0x01,
        Dungeons    = 0x02,
        Raids       = 0x04,
        PvP         = 0x08,
        RolePlaying = 0x10
    }

    [Flags]
    public enum GuildFinderOptionsAvailability
    {
        Weekdays = 0x1,
        Weekends = 0x2
    }

    [Flags]
    public enum GuildFinderOptionsRoles
    {
        Tank   = 0x1,
        Healer = 0x2,
        Dps    = 0x4
    }

    [Flags]
    public enum GuildFinderOptionsLevel
    {
        Any = 0x1,
        Max = 0x2
    }
}
