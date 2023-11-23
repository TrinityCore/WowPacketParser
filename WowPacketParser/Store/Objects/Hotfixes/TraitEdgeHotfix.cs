using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_edge")]
    public sealed record TraitEdgeHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("VisualStyle")]
        public int? VisualStyle;

        [DBFieldName("LeftTraitNodeID")]
        public int? LeftTraitNodeID;

        [DBFieldName("RightTraitNodeID")]
        public int? RightTraitNodeID;

        [DBFieldName("Type")]
        public int? Type;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_edge")]
    public sealed record TraitEdgeHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("VisualStyle")]
        public int? VisualStyle;

        [DBFieldName("LeftTraitNodeID")]
        public int? LeftTraitNodeID;

        [DBFieldName("RightTraitNodeID")]
        public int? RightTraitNodeID;

        [DBFieldName("Type")]
        public int? Type;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
