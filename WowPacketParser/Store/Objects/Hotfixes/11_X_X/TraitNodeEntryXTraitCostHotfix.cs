using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node_entry_x_trait_cost")]
    public sealed record TraitNodeEntryXTraitCostHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitNodeEntryID")]
        public uint? TraitNodeEntryID;

        [DBFieldName("TraitCostID")]
        public int? TraitCostID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
