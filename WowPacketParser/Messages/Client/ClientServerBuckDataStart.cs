namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientServerBuckDataStart
    {
        public uint RequestID;
        public byte Mpid;
        public byte NumVServers;
    }
}
