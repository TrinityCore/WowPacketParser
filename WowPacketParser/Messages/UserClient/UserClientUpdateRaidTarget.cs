namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientUpdateRaidTarget
    {
        public ulong Target;
        public byte PartyIndex;
        public byte Symbol;
    }
}
