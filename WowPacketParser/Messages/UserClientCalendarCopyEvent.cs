using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarCopyEvent
    {
        public ulong ModeratorID;
        public ulong EventID;
        public uint Date;
    }
}
