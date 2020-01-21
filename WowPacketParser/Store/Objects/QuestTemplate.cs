using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_template")]
    public sealed class QuestTemplate : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("QuestType")]
        public QuestType? QuestType;

        [DBFieldName("QuestLevel")]
        public int? QuestLevel;

        [DBFieldName("ScalingFactionGroup", TargetedDatabase.BattleForAzeroth)]
        public int? QuestScalingFactionGroup;

        [DBFieldName("MaxScalingLevel", TargetedDatabase.Legion)]
        public int? QuestMaxScalingLevel;

        [DBFieldName("QuestPackageID", TargetedDatabase.WarlordsOfDraenor)]
        public uint? QuestPackageID;

        [DBFieldName("MinLevel")]
        public int? MinLevel;

        [DBFieldName("MaxLevel", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? MaxLevel;

        [DBFieldName("QuestSortID")]
        public QuestSort? QuestSortID;

        [DBFieldName("QuestInfoID")]
        public QuestInfo? QuestInfoID;

        [DBFieldName("SuggestedGroupNum")]
        public uint? SuggestedGroupNum;

        [DBFieldName("RequiredClasses", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RequiredClasses;

        [DBFieldName("RequiredSkillId", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RequriedSkillID;

        [DBFieldName("RequiredSkillPoints", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RequiredSkillPoints;

        [DBFieldName("RequiredFactionId", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor, 2)]
        public uint?[] RequiredFactionID;

        [DBFieldName("RequiredFactionValue",TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor, 2)]
        public int?[] RequiredFactionValue;

        [DBFieldName("RewardNextQuest")]
        public uint? RewardNextQuest;

        [DBFieldName("RewardXPDifficulty")]
        public uint? RewardXPDifficulty;

        [DBFieldName("RewardXPMultiplier", TargetedDatabase.WarlordsOfDraenor)]
        public float? RewardXPMultiplier;

        [DBFieldName("RewardMoney")]
        public int? RewardMoney;

        [DBFieldName("RewardMoneyDifficulty", TargetedDatabase.WarlordsOfDraenor)]
        public uint? RewardMoneyDifficulty;

        [DBFieldName("RewardMoneyMultiplier", TargetedDatabase.WarlordsOfDraenor)]
        public float? RewardMoneyMultiplier;

        [DBFieldName("RewardBonusMoney")]
        public uint? RewardBonusMoney;

        [DBFieldName("RewardDisplaySpell", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RewardDisplaySpell;

        [DBFieldName("RewardDisplaySpell", TargetedDatabase.Legion, 3)]
        public uint?[] RewardDisplaySpellLegion;

        [DBFieldName("RewardSpell", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public int? RewardSpell;

        [DBFieldName("RewardSpell", TargetedDatabase.WarlordsOfDraenor)]
        public uint? RewardSpellWod;

        [DBFieldName("RequiredMinRepFaction", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RequiredMinRepFaction;

        [DBFieldName("RequiredMaxRepFaction", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RequiredMaxRepFaction;

        [DBFieldName("RequiredMinRepValue", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public int? RequiredMinRepValue;

        [DBFieldName("RequiredMaxRepValue", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public int? RequiredMaxRepValue;

        [DBFieldName("PrevQuestId", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public int? PrevQuestID;

        [DBFieldName("NextQuestId", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public int? NextQuestID;

        [DBFieldName("ExclusiveGroup", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public int? ExclusiveGroup;

        [DBFieldName("RewardHonor", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor)]
        public int? RewardHonor;

        [DBFieldName("RewardHonor", TargetedDatabase.WarlordsOfDraenor)]
        public uint? RewardHonorWod;

        [DBFieldName("RewardKillHonor")]
        public float? RewardKillHonor;

        [DBFieldName("StartItem")]
        public uint? StartItem;

        [DBFieldName("RewardArtifactXPDifficulty", TargetedDatabase.Legion)]
        public uint? RewardArtifactXPDifficulty;

        [DBFieldName("RewardArtifactXPMultiplier", TargetedDatabase.Legion)]
        public float? RewardArtifactXPMultiplier;

        [DBFieldName("RewardArtifactCategoryID", TargetedDatabase.Legion)]
        public uint? RewardArtifactCategoryID;

        [DBFieldName("RewardMailTemplateId", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RewardMailTemplateID;

        [DBFieldName("RewardMailDelay", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public int? RewardMailDelay;

        [DBFieldName("SourceItemCount", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? SourceItemCount;

        [DBFieldName("SourceSpellId", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? SourceSpellID;

        [DBFieldName("Flags")]
        public QuestFlags? Flags;

        [DBFieldName("SpecialFlags", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("FlagsEx", TargetedDatabase.WarlordsOfDraenor)]
        public QuestFlagsEx? FlagsEx;

        [DBFieldName("FlagsEx2", TargetedDatabase.BattleForAzeroth)]
        public QuestFlagsEx2? FlagsEx2;

        [DBFieldName("MinimapTargetMark", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? MinimapTargetMark;

        [DBFieldName("RequiredPlayerKills", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RequiredPlayerKills;

        [DBFieldName("RewardSkillId", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("RewardSkillLineID", TargetedDatabase.WarlordsOfDraenor)]
        public uint? RewardSkillLineID;

        [DBFieldName("RewardSkillPoints", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("RewardNumSkillUps", TargetedDatabase.WarlordsOfDraenor)]
        public uint? RewardNumSkillUps;

        [DBFieldName("RewardReputationMask", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RewardReputationMask;

        [DBFieldName("QuestGiverPortrait", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("PortraitGiver", TargetedDatabase.WarlordsOfDraenor)]
        public uint? QuestGiverPortrait;

        [DBFieldName("PortraitGiverMount", TargetedDatabase.BattleForAzeroth)]
        public uint? PortraitGiverMount;

        [DBFieldName("QuestTurnInPortrait", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("PortraitTurnIn", TargetedDatabase.WarlordsOfDraenor)]
        public uint? QuestTurnInPortrait;

        [DBFieldName("RewardItem", 4)]
        public uint?[] RewardItem;

        [DBFieldName("RewardAmount", 4)]
        public uint?[] RewardAmount;

        [DBFieldName("ItemDrop", 4)]
        public uint?[] ItemDrop;

        [DBFieldName("ItemDropQuantity", 4)]
        public uint?[] ItemDropQuantity;

        [DBFieldName("RewardChoiceItemID", 6)]
        public uint?[] RewardChoiceItemID;

        [DBFieldName("RewardChoiceItemQuantity", 6)]
        public uint?[] RewardChoiceItemQuantity;

        [DBFieldName("RewardChoiceItemDisplayID", TargetedDatabase.WarlordsOfDraenor, 6)]
        public uint?[] RewardChoiceItemDisplayID;

        [DBFieldName("POIContinent")]
        public uint? POIContinent;

        [DBFieldName("POIx")]
        public float? POIx;

        [DBFieldName("POIy")]
        public float? POIy;

        [DBFieldName("POIPriority", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor)]
        public uint? POIPriority;

        [DBFieldName("POIPriority", TargetedDatabase.WarlordsOfDraenor)]
        public int? POIPriorityWod;

        [DBFieldName("RewardTitle")]
        public uint? RewardTitle;

        [DBFieldName("RewardTalents", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RewardTalents;

        [DBFieldName("RewardArenaPoints")]
        public uint? RewardArenaPoints;

        [DBFieldName("RewardFactionID", 5)]
        public uint?[] RewardFactionID;

        [DBFieldName("RewardFactionValue", 5)]
        public int?[] RewardFactionValue;

        [DBFieldName("RewardFactionCapIn", TargetedDatabase.Legion, 5)]
        public int?[] RewardFactionCapIn;

        [DBFieldName("RewardFactionOverride", 5)]
        public int?[] RewardFactionOverride;

        [DBFieldName("RewardFactionFlags", TargetedDatabase.WarlordsOfDraenor)]
        public uint? RewardFactionFlags;

        [DBFieldName("AreaGroupID", TargetedDatabase.WarlordsOfDraenor)]
        public uint? AreaGroupID;

        [DBFieldName("TimeAllowed")]
        public uint? TimeAllowed;

        [DBFieldName("AllowableRaces", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor)]
        public RaceMask? AllowableRaces;

        [DBFieldName("AllowableRaces", TargetedDatabase.WarlordsOfDraenor)]
        public ulong? AllowableRacesWod;

        [DBFieldName("QuestRewardID", TargetedDatabase.Legion, TargetedDatabase.BattleForAzeroth)]
        [DBFieldName("TreasurePickerID", TargetedDatabase.BattleForAzeroth)]
        public int? QuestRewardID;

        [DBFieldName("Expansion", TargetedDatabase.Legion)]
        public int? Expansion;

        [DBFieldName("ManagedWorldStateID", TargetedDatabase.BattleForAzeroth)]
        public int? ManagedWorldStateID;

        [DBFieldName("QuestSessionBonus", TargetedDatabase.BattleForAzeroth)]
        public int? QuestSessionBonus;

        [DBFieldName("LogTitle", LocaleConstant.enUS)]
        public string LogTitle;

        [DBFieldName("LogDescription", LocaleConstant.enUS)]
        public string LogDescription;

        [DBFieldName("QuestDescription", LocaleConstant.enUS)]
        public string QuestDescription;

        [DBFieldName("AreaDescription", LocaleConstant.enUS)]
        public string AreaDescription;

        [DBFieldName("QuestCompletionLog", LocaleConstant.enUS)]
        public string QuestCompletionLog;

        [DBFieldName("RequiredNpcOrGo", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor, 4)]
        public int?[] RequiredNpcOrGo;

        [DBFieldName("RequiredNpcOrGoCount", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor, 4)]
        public uint?[] RequiredNpcOrGoCount;

        [DBFieldName("RequiredItemId", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor, 6)]
        public uint?[] RequiredItemID;

        [DBFieldName("RequiredItemCount", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor, 6)]
        public uint?[] RequiredItemCount;

        [DBFieldName("Unknown0", TargetedDatabase.Zero, TargetedDatabase.Cataclysm)]
        public uint? Unk0;

        [DBFieldName("RequiredSpell", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        public uint? RequiredSpell;

        [DBFieldName("ObjectiveText", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor, 4)]
        public string[] ObjectiveText;

        [DBFieldName("RewardCurrencyId", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor, 4)]
        [DBFieldName("RewardCurrencyID", TargetedDatabase.WarlordsOfDraenor, 4)]
        public uint?[] RewardCurrencyID;

        [DBFieldName("RewardCurrencyCount", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor, 4)]
        [DBFieldName("RewardCurrencyQty", TargetedDatabase.WarlordsOfDraenor, 4)]
        public uint?[] RewardCurrencyCount;

        [DBFieldName("RequiredCurrencyId", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor, 4)]
        public uint?[] RequiredCurrencyID;

        [DBFieldName("RequiredCurrencyCount", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor, 4)]
        public uint?[] RequiredCurrencyCount;

        [DBFieldName("QuestGiverTextWindow", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("PortraitGiverText", TargetedDatabase.WarlordsOfDraenor, LocaleConstant.enUS)]
        public string QuestGiverTextWindow;

        [DBFieldName("QuestGiverTargetName", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("PortraitGiverName", TargetedDatabase.WarlordsOfDraenor, LocaleConstant.enUS)]
        public string QuestGiverTargetName;

        [DBFieldName("QuestTurnTextWindow", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("PortraitTurnInText", TargetedDatabase.WarlordsOfDraenor, LocaleConstant.enUS)]
        public string QuestTurnTextWindow;

        [DBFieldName("QuestTurnTargetName", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("PortraitTurnInName", TargetedDatabase.WarlordsOfDraenor, LocaleConstant.enUS)]
        public string QuestTurnTargetName;

        [DBFieldName("SoundAccept", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("AcceptedSoundKitID", TargetedDatabase.WarlordsOfDraenor)]
        public uint? SoundAccept;

        [DBFieldName("SoundTurnIn", TargetedDatabase.Cataclysm, TargetedDatabase.WarlordsOfDraenor)]
        [DBFieldName("CompleteSoundKitID", TargetedDatabase.WarlordsOfDraenor)]
        public uint? SoundTurnIn;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
