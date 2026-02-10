using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("player_data_flag_character")]
    public sealed record PlayerDataFlagCharacterHotfix1200 : IDataModel
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
