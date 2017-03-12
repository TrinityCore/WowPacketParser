namespace WowPacketParser.Messages.UserRouterClient
{
    public unsafe struct UserRouterClientAuthContinuedSession
    {
        public ulong Key;
        public ulong DosResponse;
        public fixed byte Digest[20];
    }
}
