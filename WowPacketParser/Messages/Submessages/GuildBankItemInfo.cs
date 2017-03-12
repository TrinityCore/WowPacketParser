using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct GuildBankItemInfo
    {
        public int Slot;
        public ItemInstance Item;
        public int Count;
        public int EnchantmentID;
        public int Charges;
        public List<GuildBankSocketEnchant> SocketEnchant;
        public int OnUseEnchantmentID;
        public bool Locked;
        public int Flags;
    }
}
