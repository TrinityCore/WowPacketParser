using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventRemovedAlert
    {
        public ulong EventID;
        public bool ClearPending;
        public uint Date;
    }
}
