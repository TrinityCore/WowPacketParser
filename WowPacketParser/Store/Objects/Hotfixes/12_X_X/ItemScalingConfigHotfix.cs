using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_scaling_config")]
    public sealed record ItemScalingConfigHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemOffsetCurveID")]
        public int? ItemOffsetCurveID;

        [DBFieldName("ItemLevel")]
        public int? ItemLevel;

        [DBFieldName("RequiredLevel")]
        public int? RequiredLevel;

        [DBFieldName("ItemSquishEraID")]
        public int? ItemSquishEraID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
