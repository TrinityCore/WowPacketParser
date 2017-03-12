using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarUpdateEvent
    {
        public uint MaxSize;
        public ClientCalendarUpdateEventInfo EventInfo;
    }
}
