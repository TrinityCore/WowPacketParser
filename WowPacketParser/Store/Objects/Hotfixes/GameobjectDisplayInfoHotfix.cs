using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("gameobject_display_info")]
    public sealed record GameobjectDisplayInfoHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GeoBoxMinX")]
        public float? GeoBoxMinX;

        [DBFieldName("GeoBoxMinY")]
        public float? GeoBoxMinY;

        [DBFieldName("GeoBoxMinZ")]
        public float? GeoBoxMinZ;

        [DBFieldName("GeoBoxMaxX")]
        public float? GeoBoxMaxX;

        [DBFieldName("GeoBoxMaxY")]
        public float? GeoBoxMaxY;

        [DBFieldName("GeoBoxMaxZ")]
        public float? GeoBoxMaxZ;

        [DBFieldName("FileDataID")]
        public int? FileDataID;

        [DBFieldName("ObjectEffectPackageID")]
        public short? ObjectEffectPackageID;

        [DBFieldName("OverrideLootEffectScale")]
        public float? OverrideLootEffectScale;

        [DBFieldName("OverrideNameScale")]
        public float? OverrideNameScale;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("gameobject_display_info")]
    public sealed record GameobjectDisplayInfoHotfix1015 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GeoBoxMinX")]
        public float? GeoBoxMinX;

        [DBFieldName("GeoBoxMinY")]
        public float? GeoBoxMinY;

        [DBFieldName("GeoBoxMinZ")]
        public float? GeoBoxMinZ;

        [DBFieldName("GeoBoxMaxX")]
        public float? GeoBoxMaxX;

        [DBFieldName("GeoBoxMaxY")]
        public float? GeoBoxMaxY;

        [DBFieldName("GeoBoxMaxZ")]
        public float? GeoBoxMaxZ;

        [DBFieldName("FileDataID")]
        public int? FileDataID;

        [DBFieldName("ObjectEffectPackageID")]
        public short? ObjectEffectPackageID;

        [DBFieldName("OverrideLootEffectScale")]
        public float? OverrideLootEffectScale;

        [DBFieldName("OverrideNameScale")]
        public float? OverrideNameScale;

        [DBFieldName("AlternateDisplayType")]
        public int? AlternateDisplayType;

        [DBFieldName("ClientCreatureDisplayInfoID")]
        public int? ClientCreatureDisplayInfoID;

        [DBFieldName("ClientItemID")]
        public int? ClientItemID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("gameobject_display_info")]
    public sealed record GameobjectDisplayInfoHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ModelName")]
        public string ModelName;

        [DBFieldName("GeoBoxMinX")]
        public float? GeoBoxMinX;

        [DBFieldName("GeoBoxMinY")]
        public float? GeoBoxMinY;

        [DBFieldName("GeoBoxMinZ")]
        public float? GeoBoxMinZ;

        [DBFieldName("GeoBoxMaxX")]
        public float? GeoBoxMaxX;

        [DBFieldName("GeoBoxMaxY")]
        public float? GeoBoxMaxY;

        [DBFieldName("GeoBoxMaxZ")]
        public float? GeoBoxMaxZ;

        [DBFieldName("FileDataID")]
        public int? FileDataID;

        [DBFieldName("ObjectEffectPackageID")]
        public short? ObjectEffectPackageID;

        [DBFieldName("OverrideLootEffectScale")]
        public float? OverrideLootEffectScale;

        [DBFieldName("OverrideNameScale")]
        public float? OverrideNameScale;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
