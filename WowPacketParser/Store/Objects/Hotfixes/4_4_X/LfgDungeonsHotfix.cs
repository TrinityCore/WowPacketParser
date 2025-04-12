using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("lfg_dungeons")]
    public sealed record LfgDungeonsHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("MinLevel")]
        public byte? MinLevel;

        [DBFieldName("MaxLevel")]
        public ushort? MaxLevel;

        [DBFieldName("TypeID")]
        public byte? TypeID;

        [DBFieldName("Subtype")]
        public byte? Subtype;

        [DBFieldName("Faction")]
        public sbyte? Faction;

        [DBFieldName("IconTextureFileID")]
        public int? IconTextureFileID;

        [DBFieldName("RewardsBgTextureFileID")]
        public int? RewardsBgTextureFileID;

        [DBFieldName("PopupBgTextureFileID")]
        public int? PopupBgTextureFileID;

        [DBFieldName("ExpansionLevel")]
        public byte? ExpansionLevel;

        [DBFieldName("MapID")]
        public short? MapID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("MinGear")]
        public float? MinGear;

        [DBFieldName("GroupID")]
        public byte? GroupID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("RequiredPlayerConditionID")]
        public uint? RequiredPlayerConditionID;

        [DBFieldName("TargetLevel")]
        public byte? TargetLevel;

        [DBFieldName("TargetLevelMin")]
        public byte? TargetLevelMin;

        [DBFieldName("TargetLevelMax")]
        public ushort? TargetLevelMax;

        [DBFieldName("RandomID")]
        public ushort? RandomID;

        [DBFieldName("ScenarioID")]
        public ushort? ScenarioID;

        [DBFieldName("FinalEncounterID")]
        public ushort? FinalEncounterID;

        [DBFieldName("CountTank")]
        public byte? CountTank;

        [DBFieldName("CountHealer")]
        public byte? CountHealer;

        [DBFieldName("CountDamage")]
        public byte? CountDamage;

        [DBFieldName("MinCountTank")]
        public byte? MinCountTank;

        [DBFieldName("MinCountHealer")]
        public byte? MinCountHealer;

        [DBFieldName("MinCountDamage")]
        public byte? MinCountDamage;

        [DBFieldName("BonusReputationAmount")]
        public ushort? BonusReputationAmount;

        [DBFieldName("MentorItemLevel")]
        public ushort? MentorItemLevel;

        [DBFieldName("MentorCharLevel")]
        public byte? MentorCharLevel;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("lfg_dungeons")]
    public sealed record LfgDungeonsHotfix442 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("MinLevel")]
        public byte? MinLevel;

        [DBFieldName("MaxLevel")]
        public ushort? MaxLevel;

        [DBFieldName("TypeID")]
        public byte? TypeID;

        [DBFieldName("Subtype")]
        public byte? Subtype;

        [DBFieldName("Faction")]
        public sbyte? Faction;

        [DBFieldName("IconTextureFileID")]
        public int? IconTextureFileID;

        [DBFieldName("RewardsBgTextureFileID")]
        public int? RewardsBgTextureFileID;

        [DBFieldName("PopupBgTextureFileID")]
        public int? PopupBgTextureFileID;

        [DBFieldName("ExpansionLevel")]
        public byte? ExpansionLevel;

        [DBFieldName("MapID")]
        public short? MapID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("MinGear")]
        public float? MinGear;

        [DBFieldName("GroupID")]
        public byte? GroupID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("RequiredPlayerConditionID")]
        public uint? RequiredPlayerConditionID;

        [DBFieldName("TargetLevel")]
        public byte? TargetLevel;

        [DBFieldName("TargetLevelMin")]
        public byte? TargetLevelMin;

        [DBFieldName("TargetLevelMax")]
        public ushort? TargetLevelMax;

        [DBFieldName("RandomID")]
        public ushort? RandomID;

        [DBFieldName("ScenarioID")]
        public ushort? ScenarioID;

        [DBFieldName("FinalEncounterID")]
        public ushort? FinalEncounterID;

        [DBFieldName("CountTank")]
        public byte? CountTank;

        [DBFieldName("CountHealer")]
        public byte? CountHealer;

        [DBFieldName("CountDamage")]
        public byte? CountDamage;

        [DBFieldName("MinCountTank")]
        public byte? MinCountTank;

        [DBFieldName("MinCountHealer")]
        public byte? MinCountHealer;

        [DBFieldName("MinCountDamage")]
        public byte? MinCountDamage;

        [DBFieldName("MaxPremadeCountTank")]
        public byte? MaxPremadeCountTank;

        [DBFieldName("MaxPremadeCountHealer")]
        public byte? MaxPremadeCountHealer;

        [DBFieldName("MaxPremadeCountDamage")]
        public byte? MaxPremadeCountDamage;

        [DBFieldName("BonusReputationAmount")]
        public ushort? BonusReputationAmount;

        [DBFieldName("MentorItemLevel")]
        public ushort? MentorItemLevel;

        [DBFieldName("MentorCharLevel")]
        public byte? MentorCharLevel;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("lfg_dungeons_locale")]
    public sealed record LfgDungeonsLocaleHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
