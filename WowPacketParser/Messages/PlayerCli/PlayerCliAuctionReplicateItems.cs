namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliAuctionReplicateItems
    {
        public ulong Auctioneer;
        public uint ChangeNumberCursor;
        public uint Count;
        public uint ChangeNumberGlobal;
        public uint ChangeNumberTombstone;
    }
}
