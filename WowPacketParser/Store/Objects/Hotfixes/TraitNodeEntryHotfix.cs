using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node_entry")]
    public sealed record TraitNodeEntryHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitDefinitionID")]
        public int? TraitDefinitionID;

        [DBFieldName("MaxRanks")]
        public int? MaxRanks;

        [DBFieldName("NodeEntryType")]
        public byte? NodeEntryType;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_node_entry")]
    public sealed record TraitNodeEntryHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitDefinitionID")]
        public int? TraitDefinitionID;

        [DBFieldName("MaxRanks")]
        public int? MaxRanks;

        [DBFieldName("NodeEntryType")]
        public byte? NodeEntryType;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
