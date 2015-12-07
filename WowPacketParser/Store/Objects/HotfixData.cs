using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("hotfix_data")]
    public sealed class HotfixData : IDataModel
    {
        [DBFieldName("TableHash", true)]
        public DB2Hash? TableHash;

        [DBFieldName("RecordID", true)]
        public int? RecordID;

        [DBFieldName("Timestamp", true)]
        public uint? Timestamp;

        [DBFieldName("Deleted")]
        public bool? Deleted;
    }
}
