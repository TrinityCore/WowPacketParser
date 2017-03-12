using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDisplayToast
    {
        public bool BonusRoll;
        public ToastType Type;
        public uint CurrencyID;
        public byte DisplayToastMethod;
        public bool Mailed;
        public ItemInstance Item;
        public int LootSpec;
        public uint Quantity;
    }
}
