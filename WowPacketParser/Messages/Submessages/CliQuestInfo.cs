using System.Collections.Generic;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliQuestInfo
    {
        public int QuestId;
        public int QuestType;
        public int QuestLevel;
        public int QuestPackageID;
        public int QuestMinLevel;
        public int QuestSortID;
        public int QuestInfoID;
        public int SuggestedGroupNum;
        public int RewardNextQuest;
        public int RewardXPDifficulty;
        public int RewardMoney;
        public int RewardMoneyDifficulty;
        public int RewardBonusMoney;
        public int RewardDisplaySpell;
        public int RewardSpell;
        public int RewardHonor;
        public float RewardKillHonor;
        public int StartItem;
        public int Flags;
        public int FlagsEx;
        public int POIContinent;
        public float POIx;
        public float POIy;
        public int POIPriority;
        public string LogTitle;
        public string LogDescription;
        public string QuestDescription;
        public string AreaDescription;
        public int RewardTitle;
        public int RewardTalents;
        public int RewardArenaPoints;
        public int RewardSkillLineID;
        public int RewardNumSkillUps;
        public int PortraitGiver;
        public int PortraitTurnIn;
        public string PortraitGiverText;
        public string PortraitGiverName;
        public string PortraitTurnInText;
        public string PortraitTurnInName;
        public string QuestCompletionLog;
        public int RewardFactionFlags;
        public int AcceptedSoundKitID;
        public int CompleteSoundKitID;
        public int AreaGroupID;
        public int TimeAllowed;
        public List<CliQuestInfoObjective> Objectives;
        public fixed int RewardItems[4];
        public fixed int RewardAmount[4];
        public fixed int ItemDrop[4];
        public fixed int ItemDropQuantity[4];
        public CliQuestInfoChoiceItem[/*6*/] UnfilteredChoiceItems;
        public fixed int RewardFactionID[5];
        public fixed int RewardFactionValue[5];
        public fixed int RewardFactionOverride[5];
        public fixed int RewardCurrencyID[4];
        public fixed int RewardCurrencyQty[4];
    }
}
