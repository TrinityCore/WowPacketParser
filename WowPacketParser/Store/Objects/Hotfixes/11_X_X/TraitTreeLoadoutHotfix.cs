using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_tree_loadout")]
    public sealed record TraitTreeLoadoutHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitTreeID")]
        public uint? TraitTreeID;

        [DBFieldName("ChrSpecializationID")]
        public int? ChrSpecializationID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
