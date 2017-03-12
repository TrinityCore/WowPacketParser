using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTradeUpdated
    {
        public ulong Gold;
        public uint CurrentStateIndex;
        public byte WhichPlayer;
        public uint ClientStateIndex;
        public List<TradeItem> Items;
        public int CurrencyType;
        public uint ID;
        public int ProposedEnchantment;
        public int CurrencyQuantity;
    }
}
