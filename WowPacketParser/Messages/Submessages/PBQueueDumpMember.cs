namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PBQueueDumpMember
    {
        public ulong MemberGUID;
        public float AverageTeamRating;
        public float CurrentTolerance;
        public UnixTime SecondsInQueue;
    }
}
