using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("quest_template")]
    public sealed record QuestTemplate : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("QuestType")]
        public QuestType? QuestType;

        [DBFieldName("QuestLevel", TargetedDatabaseFlag.TillBattleForAzeroth)]
        public int? QuestLevel;

        [DBFieldName("ScalingFactionGroup", TargetedDatabaseFlag.BattleForAzeroth)]
        public int? QuestScalingFactionGroup;

        [DBFieldName("MaxScalingLevel", TargetedDatabaseFlag.Legion | TargetedDatabaseFlag.BattleForAzeroth)]
        public int? QuestMaxScalingLevel;

        [DBFieldName("QuestPackageID", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? QuestPackageID;

        [DBFieldName("ContentTuningID", TargetedDatabaseFlag.SinceShadowlands)]
        public int? ContentTuningID;

        [DBFieldName("MinLevel", TargetedDatabaseFlag.TillBattleForAzeroth)]
        public int? MinLevel;

        [DBFieldName("MaxLevel", TargetedDatabaseFlag.Cataclysm)]
        public uint? MaxLevel;

        [DBFieldName("QuestSortID")]
        public QuestSort? QuestSortID;

        [DBFieldName("QuestInfoID")]
        public QuestInfo? QuestInfoID;

        [DBFieldName("SuggestedGroupNum")]
        public uint? SuggestedGroupNum;

        [DBFieldName("RequiredClasses", TargetedDatabaseFlag.Cataclysm)]
        public uint? RequiredClasses;

        [DBFieldName("RequiredSkillId", TargetedDatabaseFlag.Cataclysm)]
        public uint? RequriedSkillID;

        [DBFieldName("RequiredSkillPoints", TargetedDatabaseFlag.Cataclysm)]
        public uint? RequiredSkillPoints;

        [DBFieldName("RequiredFactionId", TargetedDatabaseFlag.TillCataclysm, 2)]
        public uint?[] RequiredFactionID;

        [DBFieldName("RequiredFactionValue", TargetedDatabaseFlag.TillCataclysm, 2)]
        public int?[] RequiredFactionValue;

        [DBFieldName("RewardNextQuest")]
        public uint? RewardNextQuest;

        [DBFieldName("RewardXPDifficulty")]
        public uint? RewardXPDifficulty;

        [DBFieldName("RewardXPMultiplier", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public float? RewardXPMultiplier;

        [DBFieldName("RewardMoney", TargetedDatabaseFlag.TillBattleForAzeroth)]
        public int? RewardMoney;

        [DBFieldName("RewardMoneyDifficulty", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? RewardMoneyDifficulty;

        [DBFieldName("RewardMoneyMultiplier", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public float? RewardMoneyMultiplier;

        [DBFieldName("RewardBonusMoney")]
        public uint? RewardBonusMoney;

        [DBFieldName("RewardDisplaySpell", TargetedDatabaseFlag.TillCataclysm)]
        public uint? RewardDisplaySpell;

        [DBFieldName("RewardDisplaySpell", TargetedDatabaseFlag.Legion | TargetedDatabaseFlag.BattleForAzeroth, 3)]
        public uint?[] RewardDisplaySpellLegion;

        [DBFieldName("RewardSpell", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public int? RewardSpell;

        [DBFieldName("RewardSpell", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? RewardSpellWod;

        [DBFieldName("RequiredMinRepFaction", TargetedDatabaseFlag.Cataclysm)]
        public uint? RequiredMinRepFaction;

        [DBFieldName("RequiredMaxRepFaction", TargetedDatabaseFlag.Cataclysm)]
        public uint? RequiredMaxRepFaction;

        [DBFieldName("RequiredMinRepValue", TargetedDatabaseFlag.Cataclysm)]
        public int? RequiredMinRepValue;

        [DBFieldName("RequiredMaxRepValue", TargetedDatabaseFlag.Cataclysm)]
        public int? RequiredMaxRepValue;

        [DBFieldName("PrevQuestId", TargetedDatabaseFlag.Cataclysm)]
        public int? PrevQuestID;

        [DBFieldName("NextQuestId", TargetedDatabaseFlag.Cataclysm)]
        public int? NextQuestID;

        [DBFieldName("ExclusiveGroup", TargetedDatabaseFlag.Cataclysm)]
        public int? ExclusiveGroup;

        [DBFieldName("RewardHonor", TargetedDatabaseFlag.TillCataclysm)]
        public int? RewardHonor;

        [DBFieldName("RewardHonor", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? RewardHonorWod;

        [DBFieldName("RewardKillHonor")]
        public float? RewardKillHonor;

        [DBFieldName("StartItem")]
        public uint? StartItem;

        [DBFieldName("RewardArtifactXPDifficulty", TargetedDatabaseFlag.SinceLegion)]
        public uint? RewardArtifactXPDifficulty;

        [DBFieldName("RewardArtifactXPMultiplier", TargetedDatabaseFlag.SinceLegion)]
        public float? RewardArtifactXPMultiplier;

        [DBFieldName("RewardArtifactCategoryID", TargetedDatabaseFlag.SinceLegion)]
        public uint? RewardArtifactCategoryID;

        [DBFieldName("RewardMailTemplateId", TargetedDatabaseFlag.Cataclysm)]
        public uint? RewardMailTemplateID;

        [DBFieldName("RewardMailDelay", TargetedDatabaseFlag.Cataclysm)]
        public int? RewardMailDelay;

        [DBFieldName("SourceItemCount", TargetedDatabaseFlag.Cataclysm)]
        public uint? SourceItemCount;

        [DBFieldName("SourceSpellId", TargetedDatabaseFlag.Cataclysm)]
        public uint? SourceSpellID;

        [DBFieldName("Flags")]
        public QuestFlags? Flags;

        [DBFieldName("SpecialFlags", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("FlagsEx", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public QuestFlagsEx? FlagsEx;

        [DBFieldName("FlagsEx2", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public QuestFlagsEx2? FlagsEx2;

        [DBFieldName("MinimapTargetMark", TargetedDatabaseFlag.Cataclysm)]
        public uint? MinimapTargetMark;

        [DBFieldName("RequiredPlayerKills", TargetedDatabaseFlag.TillCataclysm)]
        public uint? RequiredPlayerKills;

        [DBFieldName("RewardSkillId", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("RewardSkillLineID", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? RewardSkillLineID;

        [DBFieldName("RewardSkillPoints", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("RewardNumSkillUps", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? RewardNumSkillUps;

        [DBFieldName("RewardReputationMask", TargetedDatabaseFlag.Cataclysm)]
        public uint? RewardReputationMask;

        [DBFieldName("QuestGiverPortrait", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("PortraitGiver", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? QuestGiverPortrait;

        [DBFieldName("PortraitGiverMount", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public uint? PortraitGiverMount;

        [DBFieldName("PortraitGiverModelSceneID", TargetedDatabaseFlag.SinceShadowlands)]
        public int? PortraitGiverModelSceneID;

        [DBFieldName("QuestTurnInPortrait", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("PortraitTurnIn", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
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

        [DBFieldName("RewardChoiceItemDisplayID", TargetedDatabaseFlag.SinceWarlordsOfDraenor, 6)]
        public uint?[] RewardChoiceItemDisplayID;

        [DBFieldName("POIContinent")]
        public uint? POIContinent;

        [DBFieldName("POIx")]
        public float? POIx;

        [DBFieldName("POIy")]
        public float? POIy;

        [DBFieldName("POIPriority", TargetedDatabaseFlag.TillCataclysm)]
        public uint? POIPriority;

        [DBFieldName("POIPriority", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public int? POIPriorityWod;

        [DBFieldName("RewardTitle")]
        public uint? RewardTitle;

        [DBFieldName("RewardTalents", TargetedDatabaseFlag.TillCataclysm)]
        public uint? RewardTalents;

        [DBFieldName("RewardArenaPoints")]
        public uint? RewardArenaPoints;

        [DBFieldName("RewardFactionID", 5)]
        public uint?[] RewardFactionID;

        [DBFieldName("RewardFactionValue", 5)]
        public int?[] RewardFactionValue;

        [DBFieldName("RewardFactionCapIn", TargetedDatabaseFlag.SinceLegion, 5)]
        public int?[] RewardFactionCapIn;

        [DBFieldName("RewardFactionOverride", 5)]
        public int?[] RewardFactionOverride;

        [DBFieldName("RewardFactionFlags", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? RewardFactionFlags;

        [DBFieldName("AreaGroupID", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? AreaGroupID;

        [DBFieldName("TimeAllowed")]
        public uint? TimeAllowed;

        [DBFieldName("AllowableRaces", TargetedDatabaseFlag.TillCataclysm)]
        public RaceMask? AllowableRaces;

        [DBFieldName("AllowableRaces", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public ulong? AllowableRacesWod;

        [DBFieldName("QuestRewardID", TargetedDatabaseFlag.Legion)]
        [DBFieldName("TreasurePickerID", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public int? QuestRewardID;

        [DBFieldName("Expansion", TargetedDatabaseFlag.SinceLegion)]
        public int? Expansion;

        [DBFieldName("ManagedWorldStateID", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public int? ManagedWorldStateID;

        [DBFieldName("QuestSessionBonus", TargetedDatabaseFlag.SinceBattleForAzeroth)]
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

        [DBFieldName("RequiredNpcOrGo", TargetedDatabaseFlag.TillCataclysm, 4)]
        public int?[] RequiredNpcOrGo;

        [DBFieldName("RequiredNpcOrGoCount", TargetedDatabaseFlag.TillCataclysm, 4)]
        public uint?[] RequiredNpcOrGoCount;

        [DBFieldName("RequiredItemId", TargetedDatabaseFlag.TillCataclysm, 6)]
        public uint?[] RequiredItemID;

        [DBFieldName("RequiredItemCount", TargetedDatabaseFlag.TillCataclysm, 6)]
        public uint?[] RequiredItemCount;

        [DBFieldName("Unknown0", TargetedDatabaseFlag.TillWrathOfTheLichKing)]
        public uint? Unk0;

        [DBFieldName("RequiredSpell", TargetedDatabaseFlag.Cataclysm)]
        public uint? RequiredSpell;

        [DBFieldName("ObjectiveText", TargetedDatabaseFlag.TillCataclysm, 4)]
        public string[] ObjectiveText;

        [DBFieldName("RewardCurrencyId", TargetedDatabaseFlag.Cataclysm, 4)]
        [DBFieldName("RewardCurrencyID", TargetedDatabaseFlag.SinceWarlordsOfDraenor, 4)]
        public uint?[] RewardCurrencyID;

        [DBFieldName("RewardCurrencyCount", TargetedDatabaseFlag.Cataclysm, 4)]
        [DBFieldName("RewardCurrencyQty", TargetedDatabaseFlag.SinceWarlordsOfDraenor, 4)]
        public uint?[] RewardCurrencyCount;

        [DBFieldName("RequiredCurrencyId", TargetedDatabaseFlag.Cataclysm, 4)]
        public uint?[] RequiredCurrencyID;

        [DBFieldName("RequiredCurrencyCount", TargetedDatabaseFlag.Cataclysm, 4)]
        public uint?[] RequiredCurrencyCount;

        [DBFieldName("QuestGiverTextWindow", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("PortraitGiverText", TargetedDatabaseFlag.SinceWarlordsOfDraenor, LocaleConstant.enUS)]
        public string QuestGiverTextWindow;

        [DBFieldName("QuestGiverTargetName", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("PortraitGiverName", TargetedDatabaseFlag.SinceWarlordsOfDraenor, LocaleConstant.enUS)]
        public string QuestGiverTargetName;

        [DBFieldName("QuestTurnTextWindow", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("PortraitTurnInText", TargetedDatabaseFlag.SinceWarlordsOfDraenor, LocaleConstant.enUS)]
        public string QuestTurnTextWindow;

        [DBFieldName("QuestTurnTargetName", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("PortraitTurnInName", TargetedDatabaseFlag.SinceWarlordsOfDraenor, LocaleConstant.enUS)]
        public string QuestTurnTargetName;

        [DBFieldName("SoundAccept", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("AcceptedSoundKitID", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? SoundAccept;

        [DBFieldName("SoundTurnIn", TargetedDatabaseFlag.Cataclysm)]
        [DBFieldName("CompleteSoundKitID", TargetedDatabaseFlag.SinceWarlordsOfDraenor)]
        public uint? SoundTurnIn;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
