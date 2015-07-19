using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("hotfix_data")]
    public sealed class HotfixData
    {
        //[DBFieldName("Timestamp")]
        public uint Timestamp;

        [DBFieldName("Deleted")]
        public bool Deleted;
    }
}
