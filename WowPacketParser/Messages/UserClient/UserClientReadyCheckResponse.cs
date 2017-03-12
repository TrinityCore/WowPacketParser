namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientReadyCheckResponse
    {
        public byte PartyIndex;
        public ulong PartyGUID;
        public bool IsReady;
    }
}
