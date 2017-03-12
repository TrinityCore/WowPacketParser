namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBuyFailed
    {
        public ulong VendorGUID;
        public uint Muid;
        public byte Reason;
    }
}
