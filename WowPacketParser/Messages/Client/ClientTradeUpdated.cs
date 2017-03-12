using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
