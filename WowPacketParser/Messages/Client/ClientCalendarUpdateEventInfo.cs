namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarUpdateEventInfo
    {
        public ulong EventID;
        public ulong ModeratorID;
        public string Title;
        public string Description;
        public byte EventType;
        public int TextureID;
        public uint Time;
        public uint Flags;
    }
}
