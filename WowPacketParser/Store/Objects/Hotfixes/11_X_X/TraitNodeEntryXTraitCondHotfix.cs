using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node_entry_x_trait_cond")]
    public sealed record TraitNodeEntryXTraitCondHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitCondID")]
        public int? TraitCondID;

        [DBFieldName("TraitNodeEntryID")]
        public uint? TraitNodeEntryID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
