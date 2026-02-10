using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node_group_x_trait_node")]
    public sealed record TraitNodeGroupXTraitNodeHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitNodeGroupID")]
        public uint? TraitNodeGroupID;

        [DBFieldName("TraitNodeID")]
        public int? TraitNodeID;

        [DBFieldName("Index")]
        public int? Index;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
