using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GarrisonFollowerStatus
    {
        None = 0,

        Favorite = 0x01,
        Exhaused = 0x02,
        Inactive = 0x04,
        Troop    = 0x08,
        NoXpGain = 0x10
    }
}
