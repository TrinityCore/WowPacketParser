namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSetLootMethod
    {
        public ulong Master;
        public int Threshold;
        public byte Method;
        public byte PartyIndex;
    }
}
