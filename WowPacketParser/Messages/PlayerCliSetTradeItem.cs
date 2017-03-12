using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetTradeItem
    {
        public byte TradeSlot;
        public byte ItemSlotInPack;
        public byte PackSlot;
    }
}
