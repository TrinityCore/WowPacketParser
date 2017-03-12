namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LootItem
    {
        public LootListItemType Type;
        public LootItemUiType UiType;
        public uint Quantity;
        public byte LootItemType;
        public byte LootListID;
        public bool CanTradeToTapList;
        public ItemInstance Loot;
    }
}
