using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_tree")]
    public sealed record TraitTreeHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitSystemID")]
        public uint? TraitSystemID;

        [DBFieldName("Unused1000_1")]
        public int? Unused1000_1;

        [DBFieldName("FirstTraitNodeID")]
        public int? FirstTraitNodeID;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Unused1000_2")]
        public float? Unused1000_2;

        [DBFieldName("Unused1000_3")]
        public float? Unused1000_3;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
