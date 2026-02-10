using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_category")]
    public sealed record SpellCategoryHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("UsesPerWeek")]
        public int? UsesPerWeek;

        [DBFieldName("MaxCharges")]
        public int? MaxCharges;

        [DBFieldName("ChargeRecoveryTime")]
        public int? ChargeRecoveryTime;

        [DBFieldName("TypeMask")]
        public int? TypeMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_category_locale")]
    public sealed record SpellCategoryLocaleHotfix1200 : IDataModel
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
