namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBFMgrStateChanged
    {
        public ulong QueueID;
        public int State;
    }
}
