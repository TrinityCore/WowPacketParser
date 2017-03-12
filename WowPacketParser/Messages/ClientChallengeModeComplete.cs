using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChallengeModeComplete
    {
        public uint MapID;
        public uint Time;
        public uint MedalEarned;
        public ChallengeModeReward Reward;
    }
}
