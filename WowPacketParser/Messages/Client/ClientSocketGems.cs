namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSocketGems
    {
        public ulong Item;
        public fixed int Sockets[3];
        public int SocketMatch;
    }
}
