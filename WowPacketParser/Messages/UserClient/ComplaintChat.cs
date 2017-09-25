namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct ComplaintChat
    {
        public uint Command;
        public uint ChannelID;
        public string MessageLog;
    }
}
