namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct TransmogrifyItem
    {
        public ulong? SrcItemGUID; // Optional
        public ulong? SrcVoidItemGUID; // Optional
        public int ItemID;
        public uint Slot;
    }
}
