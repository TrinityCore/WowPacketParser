using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("player_data_flag_account")]
    public sealed record PlayerDataFlagAccountHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("StorageIndex")]
        public int? StorageIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("player_data_flag_account")]
    public sealed record PlayerDataFlagAccountHotfix1107 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("StorageIndex")]
        public int? StorageIndex;

        [DBFieldName("Unknown1107")]
        public int? Unknown1107;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("player_data_flag_account")]
    public sealed record PlayerDataFlagAccountHotfix1125 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("StorageIndex")]
        public int? StorageIndex;

        [DBFieldName("Unknown1107")]
        public int? Unknown1107;

        [DBFieldName("Unknown1125")]
        public int? Unknown1125;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
