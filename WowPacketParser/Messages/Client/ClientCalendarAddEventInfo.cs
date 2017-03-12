using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarAddEventInfo
    {
        public string Title;
        public string Description;
        public byte EventType;
        public int TextureID;
        public uint Time;
        public uint Flags;
        public List<ClientCalendarAddEventInviteInfo> Invites;
    }
}
