using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node_x_trait_cost")]
    public sealed record TraitNodeXTraitCostHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitNodeID")]
        public uint? TraitNodeID;

        [DBFieldName("TraitCostID")]
        public int? TraitCostID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
