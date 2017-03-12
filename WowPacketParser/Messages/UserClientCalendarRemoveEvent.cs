using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarRemoveEvent
    {
        public ulong ModeratorID;
        public ulong EventID;
        public uint Flags;
    }
}
