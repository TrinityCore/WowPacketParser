namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct BattlePayPurchase
    {
        public ulong PurchaseID;
        public uint Status;
        public uint ResultCode;
        public uint ProductID;
        public string WalletName;
    }
}
