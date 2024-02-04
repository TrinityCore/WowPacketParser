using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("db_reply_data")]
    public sealed record DbReplyData : IDataModel
    {
        [DBFieldName("TableHash", true)]
        public DB2Hash? TableHash;

        [DBFieldName("RecordId", true)]
        public int? RecordID;

        [DBFieldName("Status", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.WotlkClassic)]
        public HotfixStatus? Status;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
