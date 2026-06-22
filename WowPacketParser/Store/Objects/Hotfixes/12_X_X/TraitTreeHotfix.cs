using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_tree")]
    public sealed record TraitTreeHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitSystemID")]
        public uint? TraitSystemID;

        [DBFieldName("Unused1000_1")]
        public int? Unused1000_1;

        [DBFieldName("FirstTraitNodeID")]
        public int? FirstTraitNodeID;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Unused1000_2")]
        public float? Unused1000_2;

        [DBFieldName("Unused1000_3")]
        public float? Unused1000_3;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_tree")]
    public sealed record TraitTreeHotfix1201: IDataModel
    {
        [DBFieldName("TitleText")]
        public string TitleText;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitSystemID")]
        public uint? TraitSystemID;

        [DBFieldName("BaseNodeGroup")]
        public int? BaseNodeGroup;

        [DBFieldName("FirstTraitNodeID")]
        public int? FirstTraitNodeID;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("MinZoom")]
        public float? MinZoom;

        [DBFieldName("MaxZoom")]
        public float? MaxZoom;

        [DBFieldName("UiTextureKitID")]
        public int? UiTextureKitID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_tree_locale")]
    public sealed record TraitTreeLocaleHotfix1201 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("TitleText_lang")]
        public string TitleTextLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
