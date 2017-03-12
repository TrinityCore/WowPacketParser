namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct BattlePayDistributionObject
    {
        public ulong DistributionID;
        public uint Status;
        public uint ProductID;
        public ulong TargetPlayer;
        public uint TargetVirtualRealm;
        public uint TargetNativeRealm;
        public ulong PurchaseID;
        public BattlePayProduct? Product; // Optional
        public bool Revoked;
    }
}
