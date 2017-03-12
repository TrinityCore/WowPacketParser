namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePayConfirmPurchase
    {
        public ulong CurrentPriceFixedPoint;
        public ulong PurchaseID;
        public uint ServerToken;
    }
}
