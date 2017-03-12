namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePayAckFailed
    {
        public ulong PurchaseID;
        public uint Status;
        public uint Result;
        public uint ServerToken;
    }
}
