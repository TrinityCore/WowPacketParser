namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChannelNotify
    {
        public string Sender;
        public ulong SenderGuid;
        public byte Type;
        public byte OldFlags;
        public byte NewFlags;
        public string Channel;
        public uint SenderVirtualRealm;
        public ulong TargetGuid;
        public uint TargetVirtualRealm;
        public int ChatChannelID;
    }
}
