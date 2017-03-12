using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct QuestGiverRequestItems
    {
        public ulong QuestGiverGUID;
        public int QuestGiverCreatureID;
        public int QuestID;
        public int CompEmoteDelay;
        public int CompEmoteType;
        public bool AutoLaunched;
        public int SuggestPartyMembers;
        public int MoneyToGet;
        public List<QuestObjectiveCollect> QuestObjectiveCollect;
        public List<QuestCurrency> QuestCurrency;
        public int StatusFlags;
        public fixed int QuestFlags[2];
    }
}
