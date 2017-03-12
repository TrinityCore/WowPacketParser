using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChallengeModeComplete
    {
        public uint MapID;
        public uint Time;
        public uint MedalEarned;
        public ChallengeModeReward Reward;
    }
}
