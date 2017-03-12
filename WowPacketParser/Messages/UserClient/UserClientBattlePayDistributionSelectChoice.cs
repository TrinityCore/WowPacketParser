namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePayDistributionSelectChoice
    {
        public ulong DistributionID;
        public ulong TargetCharacter;
        public uint ProductChoice;
        public uint ClientToken;
    }
}
