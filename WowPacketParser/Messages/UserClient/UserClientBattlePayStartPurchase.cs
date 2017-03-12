namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePayStartPurchase
    {
        public ulong TargetCharacter;
        public uint ProductID;
        public uint ClientToken;
    }
}
