using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("player_condition")]
    public sealed record PlayerConditionHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("FailureDescription")]
        public string FailureDescription;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("SkillLogic")]
        public uint? SkillLogic;

        [DBFieldName("LanguageID")]
        public int? LanguageID;

        [DBFieldName("MinLanguage")]
        public byte? MinLanguage;

        [DBFieldName("MaxLanguage")]
        public int? MaxLanguage;

        [DBFieldName("MaxFactionID")]
        public ushort? MaxFactionID;

        [DBFieldName("MaxReputation")]
        public byte? MaxReputation;

        [DBFieldName("ReputationLogic")]
        public uint? ReputationLogic;

        [DBFieldName("CurrentPvpFaction")]
        public sbyte? CurrentPvpFaction;

        [DBFieldName("PvpMedal")]
        public byte? PvpMedal;

        [DBFieldName("PrevQuestLogic")]
        public uint? PrevQuestLogic;

        [DBFieldName("CurrQuestLogic")]
        public uint? CurrQuestLogic;

        [DBFieldName("CurrentCompletedQuestLogic")]
        public uint? CurrentCompletedQuestLogic;

        [DBFieldName("SpellLogic")]
        public uint? SpellLogic;

        [DBFieldName("ItemLogic")]
        public uint? ItemLogic;

        [DBFieldName("ItemFlags")]
        public byte? ItemFlags;

        [DBFieldName("AuraSpellLogic")]
        public uint? AuraSpellLogic;

        [DBFieldName("WorldStateExpressionID")]
        public ushort? WorldStateExpressionID;

        [DBFieldName("WeatherID")]
        public int? WeatherID;

        [DBFieldName("PartyStatus")]
        public byte? PartyStatus;

        [DBFieldName("LifetimeMaxPVPRank")]
        public byte? LifetimeMaxPVPRank;

        [DBFieldName("AchievementLogic")]
        public uint? AchievementLogic;

        [DBFieldName("Gender")]
        public sbyte? Gender;

        [DBFieldName("NativeGender")]
        public sbyte? NativeGender;

        [DBFieldName("AreaLogic")]
        public uint? AreaLogic;

        [DBFieldName("LfgLogic")]
        public uint? LfgLogic;

        [DBFieldName("CurrencyLogic")]
        public uint? CurrencyLogic;

        [DBFieldName("QuestKillID")]
        public int? QuestKillID;

        [DBFieldName("QuestKillLogic")]
        public uint? QuestKillLogic;

        [DBFieldName("MinExpansionLevel")]
        public sbyte? MinExpansionLevel;

        [DBFieldName("MaxExpansionLevel")]
        public sbyte? MaxExpansionLevel;

        [DBFieldName("MinAvgItemLevel")]
        public int? MinAvgItemLevel;

        [DBFieldName("MaxAvgItemLevel")]
        public int? MaxAvgItemLevel;

        [DBFieldName("MinAvgEquippedItemLevel")]
        public ushort? MinAvgEquippedItemLevel;

        [DBFieldName("MaxAvgEquippedItemLevel")]
        public ushort? MaxAvgEquippedItemLevel;

        [DBFieldName("PhaseUseFlags")]
        public byte? PhaseUseFlags;

        [DBFieldName("PhaseID")]
        public ushort? PhaseID;

        [DBFieldName("PhaseGroupID")]
        public uint? PhaseGroupID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ChrSpecializationIndex")]
        public sbyte? ChrSpecializationIndex;

        [DBFieldName("ChrSpecializationRole")]
        public sbyte? ChrSpecializationRole;

        [DBFieldName("ModifierTreeID")]
        public uint? ModifierTreeID;

        [DBFieldName("PowerType")]
        public sbyte? PowerType;

        [DBFieldName("PowerTypeComp")]
        public byte? PowerTypeComp;

        [DBFieldName("PowerTypeValue")]
        public byte? PowerTypeValue;

        [DBFieldName("WeaponSubclassMask")]
        public int? WeaponSubclassMask;

        [DBFieldName("MaxGuildLevel")]
        public byte? MaxGuildLevel;

        [DBFieldName("MinGuildLevel")]
        public byte? MinGuildLevel;

        [DBFieldName("MaxExpansionTier")]
        public sbyte? MaxExpansionTier;

        [DBFieldName("MinExpansionTier")]
        public sbyte? MinExpansionTier;

        [DBFieldName("MinPVPRank")]
        public byte? MinPVPRank;

        [DBFieldName("MaxPVPRank")]
        public byte? MaxPVPRank;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("CovenantID")]
        public int? CovenantID;

        [DBFieldName("TraitNodeEntryLogic")]
        public uint? TraitNodeEntryLogic;

        [DBFieldName("SkillID", 4)]
        public ushort?[] SkillID;

        [DBFieldName("MinSkill", 4)]
        public ushort?[] MinSkill;

        [DBFieldName("MaxSkill", 4)]
        public ushort?[] MaxSkill;

        [DBFieldName("MinFactionID", 3)]
        public uint?[] MinFactionID;

        [DBFieldName("MinReputation", 3)]
        public byte?[] MinReputation;

        [DBFieldName("PrevQuestID", 4)]
        public int?[] PrevQuestID;

        [DBFieldName("CurrQuestID", 4)]
        public int?[] CurrQuestID;

        [DBFieldName("CurrentCompletedQuestID", 4)]
        public int?[] CurrentCompletedQuestID;

        [DBFieldName("SpellID", 4)]
        public int?[] SpellID;

        [DBFieldName("ItemID", 4)]
        public int?[] ItemID;

        [DBFieldName("ItemCount", 4)]
        public uint?[] ItemCount;

        [DBFieldName("Explored", 2)]
        public ushort?[] Explored;

        [DBFieldName("Time", 2)]
        public uint?[] Time;

        [DBFieldName("AuraSpellID", 4)]
        public int?[] AuraSpellID;

        [DBFieldName("AuraStacks", 4)]
        public byte?[] AuraStacks;

        [DBFieldName("Achievement", 4)]
        public ushort?[] Achievement;

        [DBFieldName("AreaID", 4)]
        public ushort?[] AreaID;

        [DBFieldName("LfgStatus", 4)]
        public byte?[] LfgStatus;

        [DBFieldName("LfgCompare", 4)]
        public byte?[] LfgCompare;

        [DBFieldName("LfgValue", 4)]
        public uint?[] LfgValue;

        [DBFieldName("CurrencyID", 4)]
        public uint?[] CurrencyID;

        [DBFieldName("CurrencyCount", 4)]
        public uint?[] CurrencyCount;

        [DBFieldName("QuestKillMonster", 6)]
        public uint?[] QuestKillMonster;

        [DBFieldName("MovementFlags", 2)]
        public int?[] MovementFlags;

        [DBFieldName("TraitNodeEntryID", 4)]
        public int?[] TraitNodeEntryID;

        [DBFieldName("TraitNodeEntryMinRank", 4)]
        public ushort?[] TraitNodeEntryMinRank;

        [DBFieldName("TraitNodeEntryMaxRank", 4)]
        public ushort?[] TraitNodeEntryMaxRank;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("player_condition_locale")]
    public sealed record PlayerConditionLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("FailureDescription_lang")]
        public string FailureDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("player_condition")]
    public sealed record PlayerConditionHotfix1020 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("FailureDescription")]
        public string FailureDescription;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("SkillLogic")]
        public uint? SkillLogic;

        [DBFieldName("LanguageID")]
        public int? LanguageID;

        [DBFieldName("MinLanguage")]
        public byte? MinLanguage;

        [DBFieldName("MaxLanguage")]
        public int? MaxLanguage;

        [DBFieldName("MaxFactionID")]
        public ushort? MaxFactionID;

        [DBFieldName("MaxReputation")]
        public byte? MaxReputation;

        [DBFieldName("ReputationLogic")]
        public uint? ReputationLogic;

        [DBFieldName("CurrentPvpFaction")]
        public sbyte? CurrentPvpFaction;

        [DBFieldName("PvpMedal")]
        public byte? PvpMedal;

        [DBFieldName("PrevQuestLogic")]
        public uint? PrevQuestLogic;

        [DBFieldName("CurrQuestLogic")]
        public uint? CurrQuestLogic;

        [DBFieldName("CurrentCompletedQuestLogic")]
        public uint? CurrentCompletedQuestLogic;

        [DBFieldName("SpellLogic")]
        public uint? SpellLogic;

        [DBFieldName("ItemLogic")]
        public uint? ItemLogic;

        [DBFieldName("ItemFlags")]
        public byte? ItemFlags;

        [DBFieldName("AuraSpellLogic")]
        public uint? AuraSpellLogic;

        [DBFieldName("WorldStateExpressionID")]
        public ushort? WorldStateExpressionID;

        [DBFieldName("WeatherID")]
        public int? WeatherID;

        [DBFieldName("PartyStatus")]
        public byte? PartyStatus;

        [DBFieldName("LifetimeMaxPVPRank")]
        public byte? LifetimeMaxPVPRank;

        [DBFieldName("AchievementLogic")]
        public uint? AchievementLogic;

        [DBFieldName("Gender")]
        public sbyte? Gender;

        [DBFieldName("NativeGender")]
        public sbyte? NativeGender;

        [DBFieldName("AreaLogic")]
        public uint? AreaLogic;

        [DBFieldName("LfgLogic")]
        public uint? LfgLogic;

        [DBFieldName("CurrencyLogic")]
        public uint? CurrencyLogic;

        [DBFieldName("QuestKillID")]
        public int? QuestKillID;

        [DBFieldName("QuestKillLogic")]
        public uint? QuestKillLogic;

        [DBFieldName("MinExpansionLevel")]
        public sbyte? MinExpansionLevel;

        [DBFieldName("MaxExpansionLevel")]
        public sbyte? MaxExpansionLevel;

        [DBFieldName("MinAvgItemLevel")]
        public int? MinAvgItemLevel;

        [DBFieldName("MaxAvgItemLevel")]
        public int? MaxAvgItemLevel;

        [DBFieldName("MinAvgEquippedItemLevel")]
        public ushort? MinAvgEquippedItemLevel;

        [DBFieldName("MaxAvgEquippedItemLevel")]
        public ushort? MaxAvgEquippedItemLevel;

        [DBFieldName("PhaseUseFlags")]
        public int? PhaseUseFlags;

        [DBFieldName("PhaseID")]
        public ushort? PhaseID;

        [DBFieldName("PhaseGroupID")]
        public uint? PhaseGroupID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ChrSpecializationIndex")]
        public sbyte? ChrSpecializationIndex;

        [DBFieldName("ChrSpecializationRole")]
        public sbyte? ChrSpecializationRole;

        [DBFieldName("ModifierTreeID")]
        public uint? ModifierTreeID;

        [DBFieldName("PowerType")]
        public sbyte? PowerType;

        [DBFieldName("PowerTypeComp")]
        public byte? PowerTypeComp;

        [DBFieldName("PowerTypeValue")]
        public byte? PowerTypeValue;

        [DBFieldName("WeaponSubclassMask")]
        public int? WeaponSubclassMask;

        [DBFieldName("MaxGuildLevel")]
        public byte? MaxGuildLevel;

        [DBFieldName("MinGuildLevel")]
        public byte? MinGuildLevel;

        [DBFieldName("MaxExpansionTier")]
        public sbyte? MaxExpansionTier;

        [DBFieldName("MinExpansionTier")]
        public sbyte? MinExpansionTier;

        [DBFieldName("MinPVPRank")]
        public byte? MinPVPRank;

        [DBFieldName("MaxPVPRank")]
        public byte? MaxPVPRank;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("CovenantID")]
        public int? CovenantID;

        [DBFieldName("TraitNodeEntryLogic")]
        public uint? TraitNodeEntryLogic;

        [DBFieldName("SkillID", 4)]
        public ushort?[] SkillID;

        [DBFieldName("MinSkill", 4)]
        public ushort?[] MinSkill;

        [DBFieldName("MaxSkill", 4)]
        public ushort?[] MaxSkill;

        [DBFieldName("MinFactionID", 3)]
        public uint?[] MinFactionID;

        [DBFieldName("MinReputation", 3)]
        public byte?[] MinReputation;

        [DBFieldName("PrevQuestID", 4)]
        public int?[] PrevQuestID;

        [DBFieldName("CurrQuestID", 4)]
        public int?[] CurrQuestID;

        [DBFieldName("CurrentCompletedQuestID", 4)]
        public int?[] CurrentCompletedQuestID;

        [DBFieldName("SpellID", 4)]
        public int?[] SpellID;

        [DBFieldName("ItemID", 4)]
        public int?[] ItemID;

        [DBFieldName("ItemCount", 4)]
        public uint?[] ItemCount;

        [DBFieldName("Explored", 2)]
        public ushort?[] Explored;

        [DBFieldName("Time", 2)]
        public uint?[] Time;

        [DBFieldName("AuraSpellID", 4)]
        public int?[] AuraSpellID;

        [DBFieldName("AuraStacks", 4)]
        public byte?[] AuraStacks;

        [DBFieldName("Achievement", 4)]
        public ushort?[] Achievement;

        [DBFieldName("AreaID", 4)]
        public ushort?[] AreaID;

        [DBFieldName("LfgStatus", 4)]
        public byte?[] LfgStatus;

        [DBFieldName("LfgCompare", 4)]
        public byte?[] LfgCompare;

        [DBFieldName("LfgValue", 4)]
        public uint?[] LfgValue;

        [DBFieldName("CurrencyID", 4)]
        public uint?[] CurrencyID;

        [DBFieldName("CurrencyCount", 4)]
        public uint?[] CurrencyCount;

        [DBFieldName("QuestKillMonster", 6)]
        public uint?[] QuestKillMonster;

        [DBFieldName("MovementFlags", 2)]
        public int?[] MovementFlags;

        [DBFieldName("TraitNodeEntryID", 4)]
        public int?[] TraitNodeEntryID;

        [DBFieldName("TraitNodeEntryMinRank", 4)]
        public ushort?[] TraitNodeEntryMinRank;

        [DBFieldName("TraitNodeEntryMaxRank", 4)]
        public ushort?[] TraitNodeEntryMaxRank;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("player_condition_locale")]
    public sealed record PlayerConditionLocaleHotfix1020 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("FailureDescription_lang")]
        public string FailureDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("player_condition")]
    public sealed record PlayerConditionHotfix340: IDataModel
    {
        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("FailureDescription")]
        public string FailureDescription;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MinLevel")]
        public ushort? MinLevel;

        [DBFieldName("MaxLevel")]
        public ushort? MaxLevel;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("SkillLogic")]
        public uint? SkillLogic;

        [DBFieldName("LanguageID")]
        public byte? LanguageID;

        [DBFieldName("MinLanguage")]
        public byte? MinLanguage;

        [DBFieldName("MaxLanguage")]
        public int? MaxLanguage;

        [DBFieldName("MaxFactionID")]
        public ushort? MaxFactionID;

        [DBFieldName("MaxReputation")]
        public byte? MaxReputation;

        [DBFieldName("ReputationLogic")]
        public uint? ReputationLogic;

        [DBFieldName("CurrentPvpFaction")]
        public sbyte? CurrentPvpFaction;

        [DBFieldName("PvpMedal")]
        public byte? PvpMedal;

        [DBFieldName("PrevQuestLogic")]
        public uint? PrevQuestLogic;

        [DBFieldName("CurrQuestLogic")]
        public uint? CurrQuestLogic;

        [DBFieldName("CurrentCompletedQuestLogic")]
        public uint? CurrentCompletedQuestLogic;

        [DBFieldName("SpellLogic")]
        public uint? SpellLogic;

        [DBFieldName("ItemLogic")]
        public uint? ItemLogic;

        [DBFieldName("ItemFlags")]
        public byte? ItemFlags;

        [DBFieldName("AuraSpellLogic")]
        public uint? AuraSpellLogic;

        [DBFieldName("WorldStateExpressionID")]
        public ushort? WorldStateExpressionID;

        [DBFieldName("WeatherID")]
        public byte? WeatherID;

        [DBFieldName("PartyStatus")]
        public byte? PartyStatus;

        [DBFieldName("LifetimeMaxPVPRank")]
        public byte? LifetimeMaxPVPRank;

        [DBFieldName("AchievementLogic")]
        public uint? AchievementLogic;

        [DBFieldName("Gender")]
        public sbyte? Gender;

        [DBFieldName("NativeGender")]
        public sbyte? NativeGender;

        [DBFieldName("AreaLogic")]
        public uint? AreaLogic;

        [DBFieldName("LfgLogic")]
        public uint? LfgLogic;

        [DBFieldName("CurrencyLogic")]
        public uint? CurrencyLogic;

        [DBFieldName("QuestKillID")]
        public uint? QuestKillID;

        [DBFieldName("QuestKillLogic")]
        public uint? QuestKillLogic;

        [DBFieldName("MinExpansionLevel")]
        public sbyte? MinExpansionLevel;

        [DBFieldName("MaxExpansionLevel")]
        public sbyte? MaxExpansionLevel;

        [DBFieldName("MinAvgItemLevel")]
        public int? MinAvgItemLevel;

        [DBFieldName("MaxAvgItemLevel")]
        public int? MaxAvgItemLevel;

        [DBFieldName("MinAvgEquippedItemLevel")]
        public ushort? MinAvgEquippedItemLevel;

        [DBFieldName("MaxAvgEquippedItemLevel")]
        public ushort? MaxAvgEquippedItemLevel;

        [DBFieldName("PhaseUseFlags")]
        public byte? PhaseUseFlags;

        [DBFieldName("PhaseID")]
        public ushort? PhaseID;

        [DBFieldName("PhaseGroupID")]
        public uint? PhaseGroupID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ChrSpecializationIndex")]
        public sbyte? ChrSpecializationIndex;

        [DBFieldName("ChrSpecializationRole")]
        public sbyte? ChrSpecializationRole;

        [DBFieldName("ModifierTreeID")]
        public uint? ModifierTreeID;

        [DBFieldName("PowerType")]
        public sbyte? PowerType;

        [DBFieldName("PowerTypeComp")]
        public byte? PowerTypeComp;

        [DBFieldName("PowerTypeValue")]
        public byte? PowerTypeValue;

        [DBFieldName("WeaponSubclassMask")]
        public int? WeaponSubclassMask;

        [DBFieldName("MaxGuildLevel")]
        public byte? MaxGuildLevel;

        [DBFieldName("MinGuildLevel")]
        public byte? MinGuildLevel;

        [DBFieldName("MaxExpansionTier")]
        public sbyte? MaxExpansionTier;

        [DBFieldName("MinExpansionTier")]
        public sbyte? MinExpansionTier;

        [DBFieldName("MinPVPRank")]
        public byte? MinPVPRank;

        [DBFieldName("MaxPVPRank")]
        public byte? MaxPVPRank;

        [DBFieldName("SkillID", 4)]
        public ushort?[] SkillID;

        [DBFieldName("MinSkill", 4)]
        public ushort?[] MinSkill;

        [DBFieldName("MaxSkill", 4)]
        public ushort?[] MaxSkill;

        [DBFieldName("MinFactionID", 3)]
        public uint?[] MinFactionID;

        [DBFieldName("MinReputation", 3)]
        public byte?[] MinReputation;

        [DBFieldName("PrevQuestID", 4)]
        public uint?[] PrevQuestID;

        [DBFieldName("CurrQuestID", 4)]
        public uint?[] CurrQuestID;

        [DBFieldName("CurrentCompletedQuestID", 4)]
        public uint?[] CurrentCompletedQuestID;

        [DBFieldName("SpellID", 4)]
        public int?[] SpellID;

        [DBFieldName("ItemID", 4)]
        public int?[] ItemID;

        [DBFieldName("ItemCount", 4)]
        public uint?[] ItemCount;

        [DBFieldName("Explored", 2)]
        public ushort?[] Explored;

        [DBFieldName("Time", 2)]
        public uint?[] Time;

        [DBFieldName("AuraSpellID", 4)]
        public int?[] AuraSpellID;

        [DBFieldName("AuraStacks", 4)]
        public byte?[] AuraStacks;

        [DBFieldName("Achievement", 4)]
        public ushort?[] Achievement;

        [DBFieldName("AreaID", 4)]
        public ushort?[] AreaID;

        [DBFieldName("LfgStatus", 4)]
        public byte?[] LfgStatus;

        [DBFieldName("LfgCompare", 4)]
        public byte?[] LfgCompare;

        [DBFieldName("LfgValue", 4)]
        public uint?[] LfgValue;

        [DBFieldName("CurrencyID", 4)]
        public uint?[] CurrencyID;

        [DBFieldName("CurrencyCount", 4)]
        public uint?[] CurrencyCount;

        [DBFieldName("QuestKillMonster", 6)]
        public uint?[] QuestKillMonster;

        [DBFieldName("MovementFlags", 2)]
        public int?[] MovementFlags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("player_condition_locale")]
    public sealed record PlayerConditionLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("FailureDescription_lang")]
        public string FailureDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
