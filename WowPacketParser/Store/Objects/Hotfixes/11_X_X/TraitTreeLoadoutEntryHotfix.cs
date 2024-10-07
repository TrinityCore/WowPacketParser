using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_tree_loadout_entry")]
    public sealed record TraitTreeLoadoutEntryHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitTreeLoadoutID")]
        public uint? TraitTreeLoadoutID;

        [DBFieldName("SelectedTraitNodeID")]
        public int? SelectedTraitNodeID;

        [DBFieldName("SelectedTraitNodeEntryID")]
        public int? SelectedTraitNodeEntryID;

        [DBFieldName("NumPoints")]
        public int? NumPoints;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
