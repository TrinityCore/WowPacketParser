using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum StatsOutputFlags
    {
        None   = 0x0,
        Local  = 0x1,
        Global = 0x2
    }
}
