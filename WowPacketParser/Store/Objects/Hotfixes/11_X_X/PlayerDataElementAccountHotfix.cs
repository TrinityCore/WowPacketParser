using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("player_data_element_account")]
    public sealed record PlayerDataElementAccountHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("StorageIndex")]
        public int? StorageIndex;

        [DBFieldName("Type")]
        public int? Type;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("player_data_element_account")]
    public sealed record PlayerDataElementAccountHotfix1125 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("StorageIndex")]
        public int? StorageIndex;

        [DBFieldName("Type")]
        public int? Type;

        [DBFieldName("Unknown1125")]
        public int? Unknown1125;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
