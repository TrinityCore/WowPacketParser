namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSetItemPurchaseData
    {
        public uint PurchaseTime;
        public uint Flags;
        public ClientItemPurchaseContents Contents;
        public ulong ItemGUID;
    }
}
