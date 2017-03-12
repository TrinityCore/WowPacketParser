using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarSendEvent
    {
        public ulong OwnerGUID;
        public uint Date;
        public uint LockDate;
        public byte GetEventType;
        public string Description;
        public string EventName;
        public int TextureID;
        public uint Flags;
        public byte EventType;
        public List<ClientCalendarEventInviteInfo> Invites;
        public ulong EventGuildID;
        public ulong EventID;
    }
}
