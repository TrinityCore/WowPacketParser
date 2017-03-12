using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
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
