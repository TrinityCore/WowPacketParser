namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSellResponse
    {
        public ulong VendorGUID;
        public ulong ItemGUID;
        public byte Reason;
    }
}
