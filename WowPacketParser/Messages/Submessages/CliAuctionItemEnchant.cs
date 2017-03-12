namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliAuctionItemEnchant
    {
        public int ID;
        public uint Expiration;
        public int Charges;
        public byte Slot;
    }
}
