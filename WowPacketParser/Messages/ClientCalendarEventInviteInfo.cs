using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventInviteInfo
    {
        public ulong Guid;
        public ulong InviteID;
        public byte Level;
        public byte Status;
        public byte Moderator;
        public byte InviteType;
        public uint ResponseTime;
        public string Notes;
    }
}
