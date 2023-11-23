using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_armor_total")]
    public sealed record ItemArmorTotalHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemLevel")]
        public short? ItemLevel;

        [DBFieldName("Cloth")]
        public float? Cloth;

        [DBFieldName("Leather")]
        public float? Leather;

        [DBFieldName("Mail")]
        public float? Mail;

        [DBFieldName("Plate")]
        public float? Plate;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_armor_total")]
    public sealed record ItemArmorTotalHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemLevel")]
        public short? ItemLevel;

        [DBFieldName("Cloth")]
        public float? Cloth;

        [DBFieldName("Leather")]
        public float? Leather;

        [DBFieldName("Mail")]
        public float? Mail;

        [DBFieldName("Plate")]
        public float? Plate;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
