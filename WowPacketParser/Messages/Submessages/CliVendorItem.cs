namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliVendorItem
    {
        public uint Muid;
        public int Type;
        public ItemInstance Item;
        public int Quantity;
        public int Price;
        public int Durability;
        public int StackCount;
        public int ExtendedCostID;
        public int PlayerConditionFailed;
        public bool DoNotFilterOnVendor;
    }
}
