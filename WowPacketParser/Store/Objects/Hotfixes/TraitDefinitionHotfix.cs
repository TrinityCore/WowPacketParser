using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_definition")]
    public sealed record TraitDefinitionHotfix1000: IDataModel
    {
        [DBFieldName("OverrideName")]
        public string OverrideName;

        [DBFieldName("OverrideSubtext")]
        public string OverrideSubtext;

        [DBFieldName("OverrideDescription")]
        public string OverrideDescription;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("OverrideIcon")]
        public int? OverrideIcon;

        [DBFieldName("OverridesSpellID")]
        public int? OverridesSpellID;

        [DBFieldName("VisibleSpellID")]
        public int? VisibleSpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_definition_locale")]
    public sealed record TraitDefinitionLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("OverrideName_lang")]
        public string OverrideNameLang;

        [DBFieldName("OverrideSubtext_lang")]
        public string OverrideSubtextLang;

        [DBFieldName("OverrideDescription_lang")]
        public string OverrideDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_definition")]
    public sealed record TraitDefinitionHotfix341: IDataModel
    {
        [DBFieldName("OverrideName")]
        public string OverrideName;

        [DBFieldName("OverrideSubtext")]
        public string OverrideSubtext;

        [DBFieldName("OverrideDescription")]
        public string OverrideDescription;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("OverrideIcon")]
        public int? OverrideIcon;

        [DBFieldName("OverridesSpellID")]
        public int? OverridesSpellID;

        [DBFieldName("VisibleSpellID")]
        public int? VisibleSpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_definition_locale")]
    public sealed record TraitDefinitionLocaleHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("OverrideName_lang")]
        public string OverrideNameLang;

        [DBFieldName("OverrideSubtext_lang")]
        public string OverrideSubtextLang;

        [DBFieldName("OverrideDescription_lang")]
        public string OverrideDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
