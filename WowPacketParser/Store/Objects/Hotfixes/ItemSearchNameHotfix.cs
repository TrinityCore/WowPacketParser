using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_search_name")]
    public sealed record ItemSearchNameHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AllowableRace")]
        public long? AllowableRace;

        [DBFieldName("Display")]
        public string Display;

        [DBFieldName("OverallQualityID")]
        public byte? OverallQualityID;

        [DBFieldName("ExpansionID")]
        public int? ExpansionID;

        [DBFieldName("MinFactionID")]
        public ushort? MinFactionID;

        [DBFieldName("MinReputation")]
        public int? MinReputation;

        [DBFieldName("AllowableClass")]
        public int? AllowableClass;

        [DBFieldName("RequiredLevel")]
        public sbyte? RequiredLevel;

        [DBFieldName("RequiredSkill")]
        public ushort? RequiredSkill;

        [DBFieldName("RequiredSkillRank")]
        public ushort? RequiredSkillRank;

        [DBFieldName("RequiredAbility")]
        public uint? RequiredAbility;

        [DBFieldName("ItemLevel")]
        public ushort? ItemLevel;

        [DBFieldName("Flags", 4)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_search_name_locale")]
    public sealed record ItemSearchNameLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Display_lang")]
        public string DisplayLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_search_name")]
    public sealed record ItemSearchNameHotfix342: IDataModel
    {
        [DBFieldName("AllowableRace")]
        public long? AllowableRace;

        [DBFieldName("Display")]
        public string Display;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OverallQualityID")]
        public byte? OverallQualityID;

        [DBFieldName("ExpansionID")]
        public sbyte? ExpansionID;

        [DBFieldName("MinFactionID")]
        public ushort? MinFactionID;

        [DBFieldName("MinReputation")]
        public int? MinReputation;

        [DBFieldName("AllowableClass")]
        public int? AllowableClass;

        [DBFieldName("RequiredLevel")]
        public sbyte? RequiredLevel;

        [DBFieldName("RequiredSkill")]
        public ushort? RequiredSkill;

        [DBFieldName("RequiredSkillRank")]
        public ushort? RequiredSkillRank;

        [DBFieldName("RequiredAbility")]
        public uint? RequiredAbility;

        [DBFieldName("ItemLevel")]
        public ushort? ItemLevel;

        [DBFieldName("Flags", 4)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_search_name_locale")]
    public sealed record ItemSearchNameLocaleHotfix342: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Display_lang")]
        public string DisplayLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
