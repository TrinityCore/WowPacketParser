namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct TradeItem
    {
        public byte Slot;
        public int EntryID;
        public int StackCount;
        public ulong GiftCreator;
        public UnwrappedTradeItem? Unwrapped; // Optional
    }
}
