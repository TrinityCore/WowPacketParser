namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LootCurrency
    {
        public uint CurrencyID;
        public uint Quantity;
        public byte LootListID;
        public LootItemUiType UiType;
    }
}
