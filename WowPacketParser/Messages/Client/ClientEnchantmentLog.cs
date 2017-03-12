namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientEnchantmentLog
    {
        public ulong Caster;
        public ulong Owner;
        public ulong ItemGUID;
        public int ItemID;
        public int EnchantSlot;
        public int Enchantment;
    }
}
