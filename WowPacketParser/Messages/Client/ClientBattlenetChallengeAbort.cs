namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlenetChallengeAbort
    {
        public uint Token;
        public bool Timeout;
    }
}
