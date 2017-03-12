using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarEventStatus
    {
        public ulong ModeratorID;
        public ulong EventID;
        public ulong InviteID;
        public ulong Guid;
        public byte Status;
    }
}
