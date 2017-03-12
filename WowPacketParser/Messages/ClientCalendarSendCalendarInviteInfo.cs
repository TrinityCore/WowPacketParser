using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarSendCalendarInviteInfo
    {
        public ulong EventID;
        public ulong InviteID;
        public ulong InviterGUID;
        public byte Status;
        public byte Moderator;
        public byte InviteType;
    }
}
