namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUserlistUpdate
    {
        public byte ChannelFlags;
        public byte UserFlags;
        public string ChannelName;
        public ulong UpdatedUserGUID;
        public int ChannelID;
    }
}
