namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePayDistributionAssignToTarget
    {
        public ulong TargetCharacter;
        public ulong DistributionID;
        public uint ProductChoice;
        public uint ClientToken;
    }
}
