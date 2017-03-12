namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliMailAttachedItem
    {
        public byte Position;
        public int AttachID;
        public ItemInstance Item;
        public int Count;
        public int Charges;
        public uint MaxDurability;
        public int Durability;
        public bool Unlocked;
        public CliMailAttachedItemEnchant[/*8*/] Enchants;
    }
}
