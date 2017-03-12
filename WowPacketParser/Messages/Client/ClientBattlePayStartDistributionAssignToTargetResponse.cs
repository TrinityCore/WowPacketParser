namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePayStartDistributionAssignToTargetResponse
    {
        public ulong DistributionID;
        public uint ClientToken;
        public uint Result;
    }
}
