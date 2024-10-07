using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node_x_trait_node_entry")]
    public sealed record TraitNodeXTraitNodeEntryHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitNodeID")]
        public uint? TraitNodeID;

        [DBFieldName("TraitNodeEntryID")]
        public int? TraitNodeEntryID;

        [DBFieldName("Index")]
        public int? Index;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
