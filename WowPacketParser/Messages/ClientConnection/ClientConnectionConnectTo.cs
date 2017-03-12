namespace WowPacketParser.Messages.ClientConnection
{
    public unsafe struct ClientConnectionConnectTo
    {
        public ulong Key;
        public uint Serial;
        public fixed byte Where[256];
        public byte Con;
    }
}
