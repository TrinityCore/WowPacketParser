using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("hotfix_optional_data")]
    public sealed class HotfixOptionalData : IDataModel
    {
        [DBFieldName("TableHash")]
        public DB2Hash TableHash;

        [DBFieldName("RecordId")]
        public int? RecordID;

        [DBFieldName("locale")]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Key")]
        public DB2Hash Key;

        [DBFieldName("Data", false, true)]
        public string Data;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
