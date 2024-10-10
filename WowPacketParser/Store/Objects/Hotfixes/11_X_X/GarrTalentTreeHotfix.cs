using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_talent_tree")]
    public sealed record GarrTalentTreeHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("GarrTypeID")]
        public sbyte? GarrTypeID;

        [DBFieldName("ClassID")]
        public int? ClassID;

        [DBFieldName("MaxTiers")]
        public sbyte? MaxTiers;

        [DBFieldName("UiOrder")]
        public sbyte? UiOrder;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("UiTextureKitID")]
        public ushort? UiTextureKitID;

        [DBFieldName("GarrTalentTreeType")]
        public int? GarrTalentTreeType;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("FeatureTypeIndex")]
        public byte? FeatureTypeIndex;

        [DBFieldName("FeatureSubtypeIndex")]
        public byte? FeatureSubtypeIndex;

        [DBFieldName("CurrencyID")]
        public int? CurrencyID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("garr_talent_tree_locale")]
    public sealed record GarrTalentTreeLocaleHotfix1100 : IDataModel
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
