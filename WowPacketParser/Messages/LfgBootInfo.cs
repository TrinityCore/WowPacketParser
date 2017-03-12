using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LfgBootInfo
    {
        public bool VoteInProgress;
        public bool VotePassed;
        public bool MyVoteCompleted;
        public bool MyVote;
        public ulong Target;
        public uint TotalVotes;
        public uint BootVotes;
        public int TimeLeft;
        public uint VotesNeeded;
        public string Reason;
    }
}
