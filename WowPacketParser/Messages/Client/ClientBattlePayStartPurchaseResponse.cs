namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePayStartPurchaseResponse
    {
        public ulong PurchaseID;
        public uint PurchaseResult;
        public uint ClientToken;
    }
}
