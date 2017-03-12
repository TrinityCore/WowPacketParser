namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildInviteDeclined
    {
        public string Name;
        public bool AutoDecline;
        public uint VirtualRealmAddress;
    }
}
