using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct QuestGiverOfferReward
    {
        public ulong QuestGiverGUID;
        public int QuestGiverCreatureID;
        public int QuestID;
        public bool AutoLaunched;
        public int SuggestedPartyMembers;
        public QuestRewards QuestRewards;
        public List<QuestDescEmote> Emotes;
        public fixed int QuestFlags[2];
    }
}
