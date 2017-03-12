namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSetTradeItem
    {
        public byte TradeSlot;
        public byte ItemSlotInPack;
        public byte PackSlot;
    }
}
