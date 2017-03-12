namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct UnwrappedTradeItem
    {
        public ItemInstance Item;
        public int EnchantID;
        public int OnUseEnchantmentID;
        public ulong Creator;
        public int Charges;
        public bool Lock;
        public uint MaxDurability;
        public uint Durability;
        public fixed int SocketEnchant[3];
    }
}
