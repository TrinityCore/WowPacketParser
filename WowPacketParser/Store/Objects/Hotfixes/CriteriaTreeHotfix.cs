using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("criteria_tree")]
    public sealed record CriteriaTreeHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Parent")]
        public uint? Parent;

        [DBFieldName("Amount")]
        public uint? Amount;

        [DBFieldName("Operator")]
        public sbyte? Operator;

        [DBFieldName("CriteriaID")]
        public uint? CriteriaID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("criteria_tree_locale")]
    public sealed record CriteriaTreeLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("criteria_tree")]
    public sealed record CriteriaTreeHotfix1015 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Parent")]
        public uint? Parent;

        [DBFieldName("Amount")]
        public uint? Amount;

        [DBFieldName("Operator")]
        public int? Operator;

        [DBFieldName("CriteriaID")]
        public uint? CriteriaID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("criteria_tree_locale")]
    public sealed record CriteriaTreeLocaleHotfix1015 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("criteria_tree")]
    public sealed record CriteriaTreeHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Parent")]
        public uint? Parent;

        [DBFieldName("Amount")]
        public uint? Amount;

        [DBFieldName("Operator")]
        public sbyte? Operator;

        [DBFieldName("CriteriaID")]
        public uint? CriteriaID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("Flags")]
        public short? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("criteria_tree_locale")]
    public sealed record CriteriaTreeLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("criteria_tree")]
    public sealed record CriteriaTreeHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Parent")]
        public uint? Parent;

        [DBFieldName("Amount")]
        public uint? Amount;

        [DBFieldName("Operator")]
        public int? Operator;

        [DBFieldName("CriteriaID")]
        public uint? CriteriaID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("criteria_tree_locale")]
    public sealed record CriteriaTreeLocaleHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
