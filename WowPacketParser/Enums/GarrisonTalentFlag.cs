using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum GarrisonTalentFlag : int
    {
        Researched  = 0x1,
        Respec      = 0x2,
        Researching = 0x4,
        Ongoing     = 0x8,
        DontReset   = 0x10,
    }
}
