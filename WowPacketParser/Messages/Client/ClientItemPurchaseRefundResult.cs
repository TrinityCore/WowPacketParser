namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientItemPurchaseRefundResult
    {
        public byte Result;
        public ulong ItemGUID;
        public ClientItemPurchaseContents? Contents; // Optional
    }
}
