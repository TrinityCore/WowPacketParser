namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChannelNotifyJoined
    {
        public string ChannelWelcomeMsg;
        public int ChatChannelID;
        public int InstanceID;
        public byte ChannelFlags;
        public string Channel;
    }
}
