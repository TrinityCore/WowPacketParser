namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePayConfirmPurchaseResponse
    {
        public ulong ClientCurrentPriceFixedPoint;
        public bool ConfirmPurchase;
        public uint ServerToken;
    }
}
