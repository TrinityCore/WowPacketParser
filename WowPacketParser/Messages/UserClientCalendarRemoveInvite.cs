using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarRemoveInvite
    {
        public ulong ModeratorID;
        public ulong Guid;
        public ulong EventID;
        public ulong InviteID;
    }
}
