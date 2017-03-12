namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBuySucceeded
    {
        public ulong VendorGUID;
        public uint Muid;
        public uint QuantityBought;
        public int NewQuantity;
    }
}
