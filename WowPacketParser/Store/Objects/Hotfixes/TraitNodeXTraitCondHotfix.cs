using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node_x_trait_cond")]
    public sealed record TraitNodeXTraitCondHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitCondID")]
        public int? TraitCondID;

        [DBFieldName("TraitNodeID")]
        public int? TraitNodeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_node_x_trait_cond")]
    public sealed record TraitNodeXTraitCondHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitCondID")]
        public int? TraitCondID;

        [DBFieldName("TraitNodeID")]
        public int? TraitNodeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
