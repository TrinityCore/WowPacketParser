namespace WowPacketParser.Messages.UserClient.BattlePay
{
    public unsafe struct DistributionSelectChoice
    {
        public ulong DistributionID;
        public ulong TargetCharacter;
        public uint ProductChoice;
        public uint ClientToken;
    }
}
