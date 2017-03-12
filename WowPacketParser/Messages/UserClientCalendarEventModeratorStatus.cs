using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarEventModeratorStatus
    {
        public ulong InviteID;
        public ulong EventID;
        public ulong Guid;
        public ulong ModeratorID;
        public byte Status;
    }
}
