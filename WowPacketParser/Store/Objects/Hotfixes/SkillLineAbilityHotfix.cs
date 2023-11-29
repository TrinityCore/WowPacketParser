using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("skill_line_ability")]
    public sealed record SkillLineAbilityHotfix1000: IDataModel
    {
        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("AbilityVerb")]
        public string AbilityVerb;

        [DBFieldName("AbilityAllVerb")]
        public string AbilityAllVerb;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SkillLine")]
        public short? SkillLine;

        [DBFieldName("Spell")]
        public int? Spell;

        [DBFieldName("MinSkillLineRank")]
        public short? MinSkillLineRank;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("SupercedesSpell")]
        public int? SupercedesSpell;

        [DBFieldName("AcquireMethod")]
        public sbyte? AcquireMethod;

        [DBFieldName("TrivialSkillLineRankHigh")]
        public short? TrivialSkillLineRankHigh;

        [DBFieldName("TrivialSkillLineRankLow")]
        public short? TrivialSkillLineRankLow;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("NumSkillUps")]
        public sbyte? NumSkillUps;

        [DBFieldName("UniqueBit")]
        public short? UniqueBit;

        [DBFieldName("TradeSkillCategoryID")]
        public short? TradeSkillCategoryID;

        [DBFieldName("SkillupSkillLineID")]
        public short? SkillupSkillLineID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("skill_line_ability_locale")]
    public sealed record SkillLineAbilityLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("AbilityVerb_lang")]
        public string AbilityVerbLang;

        [DBFieldName("AbilityAllVerb_lang")]
        public string AbilityAllVerbLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("skill_line_ability")]
    public sealed record SkillLineAbilityHotfix340: IDataModel
    {
        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SkillLine")]
        public short? SkillLine;

        [DBFieldName("Spell")]
        public int? Spell;

        [DBFieldName("MinSkillLineRank")]
        public short? MinSkillLineRank;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("SupercedesSpell")]
        public int? SupercedesSpell;

        [DBFieldName("AcquireMethod")]
        public sbyte? AcquireMethod;

        [DBFieldName("TrivialSkillLineRankHigh")]
        public short? TrivialSkillLineRankHigh;

        [DBFieldName("TrivialSkillLineRankLow")]
        public short? TrivialSkillLineRankLow;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("NumSkillUps")]
        public sbyte? NumSkillUps;

        [DBFieldName("UniqueBit")]
        public short? UniqueBit;

        [DBFieldName("TradeSkillCategoryID")]
        public short? TradeSkillCategoryID;

        [DBFieldName("SkillupSkillLineID")]
        public short? SkillupSkillLineID;

        [DBFieldName("CharacterPoints", 2)]
        public int?[] CharacterPoints;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("skill_line_ability_locale")]
    public sealed record SkillLineAbilityLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
