using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChallengeModeDeleteLeaderResult
    {
        public int MapID;
        public uint AttemptID;
        public bool Success;
    }
}
