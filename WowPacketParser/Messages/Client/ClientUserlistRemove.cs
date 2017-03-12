namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUserlistRemove
    {
        public byte ChannelFlags;
        public string ChannelName;
        public int ChannelID;
        public ulong RemovedUserGUID;
    }
}
