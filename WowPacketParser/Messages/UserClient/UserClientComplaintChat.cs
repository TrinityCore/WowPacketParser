namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientComplaintChat
    {
        public uint Command;
        public uint ChannelID;
        public string MessageLog;
    }
}
