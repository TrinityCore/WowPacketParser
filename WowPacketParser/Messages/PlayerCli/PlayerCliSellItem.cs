namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSellItem
    {
        public ulong ItemGUID;
        public ulong VendorGUID;
        public int Amount;
    }
}
