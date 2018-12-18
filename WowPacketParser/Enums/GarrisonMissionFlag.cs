using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GarrisonMissionFlag : uint
    {
        None           = 0x00,
        IsRare         = 0x01,
        IsElite        = 0x02,
        AppliesFatigue = 0x04,
        AlwaysFail     = 0x08,
        IsZoneSupport  = 0x10
    }
}
