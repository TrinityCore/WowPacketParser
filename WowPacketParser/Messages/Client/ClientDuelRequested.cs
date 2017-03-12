namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDuelRequested
    {
        public ulong ArbiterGUID;
        public ulong RequestedByGUID;
    }
}
