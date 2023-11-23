using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("import_price_armor")]
    public sealed record ImportPriceArmorHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClothModifier")]
        public float? ClothModifier;

        [DBFieldName("LeatherModifier")]
        public float? LeatherModifier;

        [DBFieldName("ChainModifier")]
        public float? ChainModifier;

        [DBFieldName("PlateModifier")]
        public float? PlateModifier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("import_price_armor")]
    public sealed record ImportPriceArmorHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClothModifier")]
        public float? ClothModifier;

        [DBFieldName("LeatherModifier")]
        public float? LeatherModifier;

        [DBFieldName("ChainModifier")]
        public float? ChainModifier;

        [DBFieldName("PlateModifier")]
        public float? PlateModifier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
