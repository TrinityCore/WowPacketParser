using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarAddEvent
    {
        public uint MaxSize;
        public ClientCalendarAddEventInfo EventInfo;
    }
}
