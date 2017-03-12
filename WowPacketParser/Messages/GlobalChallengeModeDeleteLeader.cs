using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalChallengeModeDeleteLeader
    {
        public int MapID;
        public uint AttemptID;
    }
}
