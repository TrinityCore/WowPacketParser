namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChannelNotifyLeft
    {
        public string Channel;
        public int ChatChannelID;
        public bool Suspended;
    }
}
