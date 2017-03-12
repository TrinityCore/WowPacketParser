namespace WowPacketParser.Messages.ClientConnection
{
    public unsafe struct ClientConnectionAuthChallenge
    {
        public uint Challenge;
        public fixed uint DosChallenge[8];
        public byte DosZeroBits;
    }
}
