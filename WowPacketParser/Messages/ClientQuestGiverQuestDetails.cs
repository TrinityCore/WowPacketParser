using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestGiverQuestDetails
    {
        public string PortraitTurnInName;
        public bool DisplayPopup;
        public string PortraitGiverName;
        public List<CliQuestInfoObjectiveSimple> Objectives;
        public string PortraitGiverText;
        public bool StartCheat;
        public QuestRewards QuestRewards;
        public ulong QuestGiverGUID;
        public int QuestID;
        public string QuestTitle;
        public bool AutoLaunched;
        public List<QuestDescEmote> DescEmotes;
        public int QuestPackageID;
        public int PortraitGiver;
        public string DescriptionText;
        public ulong InformUnit;
        public int SuggestedPartyMembers;
        public int PortraitTurnIn;
        public List<int> LearnSpells;
        public string PortraitTurnInText;
        public string LogDescription;
        public fixed int QuestFlags[2];
    }
}
