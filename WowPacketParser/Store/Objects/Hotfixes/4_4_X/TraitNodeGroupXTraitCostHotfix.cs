using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node_group_x_trait_cost")]
    public sealed record TraitNodeGroupXTraitCostHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitNodeGroupID")]
        public int? TraitNodeGroupID;

        [DBFieldName("TraitCostID")]
        public int? TraitCostID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
