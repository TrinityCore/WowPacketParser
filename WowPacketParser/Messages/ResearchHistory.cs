using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ResearchHistory
    {
        public int ProjectID;
        public UnixTime FirstCompleted;
        public int CompletionCount;
    }
}
