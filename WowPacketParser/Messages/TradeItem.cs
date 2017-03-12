using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct TradeItem
    {
        public byte Slot;
        public int EntryID;
        public int StackCount;
        public ulong GiftCreator;
        public UnwrappedTradeItem Unwrapped; // Optional
    }
}
