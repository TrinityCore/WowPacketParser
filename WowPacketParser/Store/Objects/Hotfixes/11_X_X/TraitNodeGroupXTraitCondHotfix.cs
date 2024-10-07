using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node_group_x_trait_cond")]
    public sealed record TraitNodeGroupXTraitCondHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitCondID")]
        public int? TraitCondID;

        [DBFieldName("TraitNodeGroupID")]
        public uint? TraitNodeGroupID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
