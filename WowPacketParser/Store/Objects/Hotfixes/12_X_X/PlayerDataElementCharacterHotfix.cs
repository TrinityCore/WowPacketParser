using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("player_data_element_character")]
    public sealed record PlayerDataElementCharacterHotfix1200 : IDataModel
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
