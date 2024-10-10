using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_tree_x_trait_cost")]
    public sealed record TraitTreeXTraitCostHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitTreeID")]
        public uint? TraitTreeID;

        [DBFieldName("TraitCostID")]
        public int? TraitCostID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
