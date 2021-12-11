using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("sniff_data")]
    public sealed record SniffData : IDataModel
    {
        [DBFieldName("Build", true)]
        public int Build = ClientVersion.BuildInt;

        [DBFieldName("SniffName", true)]
        public string SniffName;

        [DBFieldName("ObjectType", true)]
        public StoreNameType ObjectType;

        [DBFieldName("Id", true)]
        public int Id;

        [DBFieldName("Data")]
        public string Data;
    }
}
