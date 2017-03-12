namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarSendCalendarEventInfo
    {
        public ulong EventID;
        public string EventName;
        public byte EventType;
        public uint Date;
        public uint Flags;
        public int TextureID;
        public ulong EventGuildID;
        public ulong OwnerGUID;
    }
}
