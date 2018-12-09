using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("hotfix_blob")]
    public sealed class HotfixBlob : IDataModel
    {
        [DBFieldName("TableHash", true)]
        public DB2Hash? TableHash;

        [DBFieldName("RecordId", true)]
        public int? RecordID;

        [DBFieldName("Blob", false, true)]
        public string Blob;
    }
}
