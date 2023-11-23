using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("artifact_category")]
    public sealed record ArtifactCategoryHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("XpMultCurrencyID")]
        public short? XpMultCurrencyID;

        [DBFieldName("XpMultCurveID")]
        public short? XpMultCurveID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_category")]
    public sealed record ArtifactCategoryHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("XpMultCurrencyID")]
        public short? XpMultCurrencyID;

        [DBFieldName("XpMultCurveID")]
        public short? XpMultCurveID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
