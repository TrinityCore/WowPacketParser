using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
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
    [Hotfix]
    [DBTableName("gameobject_display_info")]
    public sealed record GameobjectDisplayInfoHotfix344: IDataModel
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

        [DBFieldName("Field115456400006")]
        public ushort? Field115456400006;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
