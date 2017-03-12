namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct VoidItem
    {
        public ulong Guid;
        public ulong Creator;
        public uint Slot;
        public ItemInstance Item;
    }
}
