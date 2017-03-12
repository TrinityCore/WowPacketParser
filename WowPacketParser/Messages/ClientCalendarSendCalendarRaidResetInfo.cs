using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarSendCalendarRaidResetInfo
    {
        public int MapID;
        public uint Duration;
        public int Offset;
    }
}
