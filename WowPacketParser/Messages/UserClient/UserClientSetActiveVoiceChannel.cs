namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSetActiveVoiceChannel
    {
        public byte ChannelType;
        public string ChannelName;
    }
}
