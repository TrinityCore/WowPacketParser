using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarEventInviteAlert
    {
        public byte Status;
        public ulong OwnerGUID;
        public byte ModeratorStatus;
        public ulong EventGuildID;
        public ulong InvitedByGUID;
        public uint Flags;
        public string EventName;
        public int TextureID;
        public ulong InviteID;
        public byte EventType;
        public ulong EventID;
        public uint Date;
    }
}
