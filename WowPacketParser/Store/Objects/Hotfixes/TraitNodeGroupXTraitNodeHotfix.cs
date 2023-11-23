using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node_group_x_trait_node")]
    public sealed record TraitNodeGroupXTraitNodeHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitNodeGroupID")]
        public int? TraitNodeGroupID;

        [DBFieldName("TraitNodeID")]
        public int? TraitNodeID;

        [DBFieldName("Index")]
        public int? Index;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_node_group_x_trait_node")]
    public sealed record TraitNodeGroupXTraitNodeHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitNodeGroupID")]
        public int? TraitNodeGroupID;

        [DBFieldName("TraitNodeID")]
        public int? TraitNodeID;

        [DBFieldName("Index")]
        public int? Index;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
