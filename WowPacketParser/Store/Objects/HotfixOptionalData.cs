using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("hotfix_optional_data")]
    public sealed class HotfixOptionalData : IDataModel
    {
        [DBFieldName("TableHash", true)]
        public DB2Hash TableHash;

        [DBFieldName("RecordId", true)]
        public int? RecordID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Data", false, true)]
        public string Data;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
