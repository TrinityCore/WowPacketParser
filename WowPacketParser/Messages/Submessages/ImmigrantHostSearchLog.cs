namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ImmigrantHostSearchLog
    {
        public ulong PartyMember;
        public bool IsLeader;
        public uint Realm;
        public uint ImmigrantPop;
        public ImmigrationState State;
    }
}
