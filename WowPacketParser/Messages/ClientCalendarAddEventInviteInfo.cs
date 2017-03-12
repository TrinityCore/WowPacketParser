using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarAddEventInviteInfo
    {
        public ulong Guid;
        public byte Status;
        public byte Moderator;
    }
}
