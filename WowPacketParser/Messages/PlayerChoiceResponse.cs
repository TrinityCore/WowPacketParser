using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerChoiceResponse
    {
        public int ResponseID;
        public string Answer;
        public string Description;
        public int ChoiceArtFileID;
        public PlayerChoiceResponseReward Reward; // Optional
    }
}
