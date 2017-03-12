namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientFriendStatus
    {
        public uint VirtualRealmAddress;
        public string Notes;
        public uint ClassID;
        public byte Status;
        public ulong Guid;
        public uint Level;
        public uint AreaID;
        public byte FriendResult;
    }
}
