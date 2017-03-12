using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientArchaeologySurveryCast
    {
        public int ResearchBranchID;
        public bool SuccessfulFind;
        public uint NumFindsCompleted;
        public uint TotalFinds;
    }
}
