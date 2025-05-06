using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("lfg_dungeons")]
    public sealed record LfgDungeonsHotfix340: IDataModel
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
    [DBTableName("lfg_dungeons_locale")]
    public sealed record LfgDungeonsLocaleHotfix340: IDataModel
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
    [Hotfix]
    [DBTableName("lfg_dungeons")]
    public sealed record LfgDungeonsHotfix344: IDataModel
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

        [DBFieldName("Field115759706005")]
        public byte? Field115759706005;

        [DBFieldName("Faction")]
        public sbyte? Faction;

        [DBFieldName("Field115759706007")]
        public int? Field115759706007;

        [DBFieldName("Field115759706008")]
        public int? Field115759706008;

        [DBFieldName("Field115759706009")]
        public int? Field115759706009;

        [DBFieldName("Field115759706010")]
        public byte? Field115759706010;

        [DBFieldName("Field115759706011")]
        public short? Field115759706011;

        [DBFieldName("Field115759706012")]
        public byte? Field115759706012;

        [DBFieldName("MinGear")]
        public float? MinGear;

        [DBFieldName("Field115759706014")]
        public byte? Field115759706014;

        [DBFieldName("Field115759706015")]
        public byte? Field115759706015;

        [DBFieldName("Field115759706016")]
        public uint? Field115759706016;

        [DBFieldName("Field115759706017")]
        public byte? Field115759706017;

        [DBFieldName("Field115759706018")]
        public byte? Field115759706018;

        [DBFieldName("Field115759706019")]
        public ushort? Field115759706019;

        [DBFieldName("Field115759706020")]
        public ushort? Field115759706020;

        [DBFieldName("Field115759706021")]
        public ushort? Field115759706021;

        [DBFieldName("Field115759706022")]
        public ushort? Field115759706022;

        [DBFieldName("Field115759706023")]
        public byte? Field115759706023;

        [DBFieldName("Field115759706024")]
        public byte? Field115759706024;

        [DBFieldName("Field115759706025")]
        public byte? Field115759706025;

        [DBFieldName("Field115759706026")]
        public byte? Field115759706026;

        [DBFieldName("Field115759706027")]
        public byte? Field115759706027;

        [DBFieldName("Field115759706028")]
        public byte? Field115759706028;

        [DBFieldName("Field115759706029")]
        public byte? Field115759706029;

        [DBFieldName("Field115759706030")]
        public byte? Field115759706030;

        [DBFieldName("Field115759706031")]
        public byte? Field115759706031;

        [DBFieldName("Field115759706032")]
        public ushort? Field115759706032;

        [DBFieldName("Field115759706033")]
        public ushort? Field115759706033;

        [DBFieldName("Field115759706034")]
        public byte? Field115759706034;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("lfg_dungeons_locale")]
    public sealed record LfgDungeonsLocaleHotfix344: IDataModel
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
