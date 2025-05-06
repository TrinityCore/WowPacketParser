using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("currency_types")]
    public sealed record CurrencyTypesHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("CategoryID")]
        public byte? CategoryID;

        [DBFieldName("InventoryIconFileID")]
        public int? InventoryIconFileID;

        [DBFieldName("SpellWeight")]
        public uint? SpellWeight;

        [DBFieldName("SpellCategory")]
        public byte? SpellCategory;

        [DBFieldName("MaxQty")]
        public uint? MaxQty;

        [DBFieldName("MaxEarnablePerWeek")]
        public uint? MaxEarnablePerWeek;

        [DBFieldName("Quality")]
        public sbyte? Quality;

        [DBFieldName("FactionID")]
        public int? FactionID;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("currency_types_locale")]
    public sealed record CurrencyTypesLocaleHotfix340: IDataModel
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
    [DBTableName("currency_types")]
    public sealed record CurrencyTypesHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("CategoryID")]
        public byte? CategoryID;

        [DBFieldName("InventoryIconFileID")]
        public int? InventoryIconFileID;

        [DBFieldName("SpellWeight")]
        public uint? SpellWeight;

        [DBFieldName("SpellCategory")]
        public byte? SpellCategory;

        [DBFieldName("MaxQty")]
        public uint? MaxQty;

        [DBFieldName("MaxEarnablePerWeek")]
        public uint? MaxEarnablePerWeek;

        [DBFieldName("Quality")]
        public sbyte? Quality;

        [DBFieldName("FactionID")]
        public int? FactionID;

        [DBFieldName("AwardConditionID")]
        public int? AwardConditionID;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("currency_types_locale")]
    public sealed record CurrencyTypesLocaleHotfix343: IDataModel
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
    [DBTableName("currency_types")]
    public sealed record CurrencyTypesHotfix344: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("CategoryID")]
        public byte? CategoryID;

        [DBFieldName("InventoryIconFileID")]
        public int? InventoryIconFileID;

        [DBFieldName("SpellWeight")]
        public uint? SpellWeight;

        [DBFieldName("SpellCategory")]
        public byte? SpellCategory;

        [DBFieldName("MaxQty")]
        public uint? MaxQty;

        [DBFieldName("MaxEarnablePerWeek")]
        public uint? MaxEarnablePerWeek;

        [DBFieldName("Quality")]
        public sbyte? Quality;

        [DBFieldName("FactionID")]
        public int? FactionID;

        [DBFieldName("Field115355112010")]
        public int? Field115355112010;

        [DBFieldName("Field115355112011")]
        public int? Field115355112011;

        [DBFieldName("Field115456400012")]
        public float? Field115456400012;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("currency_types_locale")]
    public sealed record CurrencyTypesLocaleHotfix344: IDataModel
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
