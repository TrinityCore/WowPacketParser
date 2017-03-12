namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientReadyCheckCompleted
    {
        public ulong PartyGUID;
        public byte PartyIndex;
    }
}
