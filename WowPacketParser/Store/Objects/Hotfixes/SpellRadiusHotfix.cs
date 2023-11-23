using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_radius")]
    public sealed record SpellRadiusHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Radius")]
        public float? Radius;

        [DBFieldName("RadiusPerLevel")]
        public float? RadiusPerLevel;

        [DBFieldName("RadiusMin")]
        public float? RadiusMin;

        [DBFieldName("RadiusMax")]
        public float? RadiusMax;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_radius")]
    public sealed record SpellRadiusHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Radius")]
        public float? Radius;

        [DBFieldName("RadiusPerLevel")]
        public float? RadiusPerLevel;

        [DBFieldName("RadiusMin")]
        public float? RadiusMin;

        [DBFieldName("RadiusMax")]
        public float? RadiusMax;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
