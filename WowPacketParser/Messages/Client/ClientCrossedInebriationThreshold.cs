namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCrossedInebriationThreshold
    {
        public ulong Guid;
        public int ItemID;
        public int Threshold;
    }
}
