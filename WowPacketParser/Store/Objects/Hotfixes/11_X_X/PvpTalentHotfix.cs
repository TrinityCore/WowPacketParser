using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("pvp_talent")]
    public sealed record PvpTalentHotfix1100: IDataModel
    {
        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpecID")]
        public uint? SpecID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("OverridesSpellID")]
        public int? OverridesSpellID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ActionBarSpellID")]
        public int? ActionBarSpellID;

        [DBFieldName("PvpTalentCategoryID")]
        public int? PvpTalentCategoryID;

        [DBFieldName("LevelRequired")]
        public int? LevelRequired;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("pvp_talent_locale")]
    public sealed record PvpTalentLocaleHotfix1100: IDataModel
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
