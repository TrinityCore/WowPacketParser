namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBFMgrEjected
    {
        public ulong QueueID;
        public bool Relocated;
        public sbyte Reason;
        public sbyte BattleState;
    }
}
