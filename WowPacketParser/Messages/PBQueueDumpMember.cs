using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PBQueueDumpMember
    {
        public ulong MemberGUID;
        public float AverageTeamRating;
        public float CurrentTolerance;
        public UnixTime SecondsInQueue;
    }
}
