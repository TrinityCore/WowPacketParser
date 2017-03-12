namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientReadyCheckResponse
    {
        public bool IsReady;
        public ulong Player;
        public ulong PartyGUID;
    }
}
