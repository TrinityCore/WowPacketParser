namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBFMgrQueueRequestResponse
    {
        public sbyte Result;
        public sbyte BattleState;
        public ulong FailedPlayerGUID;
        public ulong QueueID;
        public bool LoggingIn;
        public int AreaID;
    }
}
