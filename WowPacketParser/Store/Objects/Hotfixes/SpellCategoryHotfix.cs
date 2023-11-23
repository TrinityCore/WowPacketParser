using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_category")]
    public sealed record SpellCategoryHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("UsesPerWeek")]
        public byte? UsesPerWeek;

        [DBFieldName("MaxCharges")]
        public sbyte? MaxCharges;

        [DBFieldName("ChargeRecoveryTime")]
        public int? ChargeRecoveryTime;

        [DBFieldName("TypeMask")]
        public int? TypeMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_category_locale")]
    public sealed record SpellCategoryLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_category")]
    public sealed record SpellCategoryHotfix1015 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("UsesPerWeek")]
        public byte? UsesPerWeek;

        [DBFieldName("MaxCharges")]
        public sbyte? MaxCharges;

        [DBFieldName("ChargeRecoveryTime")]
        public int? ChargeRecoveryTime;

        [DBFieldName("TypeMask")]
        public int? TypeMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_category_locale")]
    public sealed record SpellCategoryLocaleHotfix1015 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_category")]
    public sealed record SpellCategoryHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("UsesPerWeek")]
        public byte? UsesPerWeek;

        [DBFieldName("MaxCharges")]
        public sbyte? MaxCharges;

        [DBFieldName("ChargeRecoveryTime")]
        public int? ChargeRecoveryTime;

        [DBFieldName("TypeMask")]
        public int? TypeMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_category_locale")]
    public sealed record SpellCategoryLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("spell_category")]
    public sealed record SpellCategoryHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("UsesPerWeek")]
        public byte? UsesPerWeek;

        [DBFieldName("MaxCharges")]
        public sbyte? MaxCharges;

        [DBFieldName("ChargeRecoveryTime")]
        public int? ChargeRecoveryTime;

        [DBFieldName("TypeMask")]
        public int? TypeMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_category_locale")]
    public sealed record SpellCategoryLocaleHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
