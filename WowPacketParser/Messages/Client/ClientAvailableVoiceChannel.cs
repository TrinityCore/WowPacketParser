namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAvailableVoiceChannel
    {
        public byte SessionType;
        public ulong LocalGUID;
        public ulong SessionGUID;
        public string ChannelName;
    }
}
