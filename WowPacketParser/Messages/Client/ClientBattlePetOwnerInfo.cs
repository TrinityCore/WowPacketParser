namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePetOwnerInfo
    {
        public ulong Guid;
        public uint PlayerVirtualRealm;
        public uint PlayerNativeRealm;
    }
}
