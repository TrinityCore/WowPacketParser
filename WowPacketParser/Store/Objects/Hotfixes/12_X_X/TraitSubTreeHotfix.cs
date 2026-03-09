using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_sub_tree")]
    public sealed record TraitSubTreeHotfix1200: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UiTextureAtlasElementID")]
        public int? UiTextureAtlasElementID;

        [DBFieldName("TraitTreeID")]
        public uint? TraitTreeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_sub_tree_locale")]
    public sealed record TraitSubTreeLocaleHotfix1200: IDataModel
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
