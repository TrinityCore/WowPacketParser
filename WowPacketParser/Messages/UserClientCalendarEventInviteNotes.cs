using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarEventInviteNotes
    {
        public ulong EventID;
        public ulong Guid;
        public ulong InviteID;
        public ulong ModeratorID;
        public string Notes;
    }
}
