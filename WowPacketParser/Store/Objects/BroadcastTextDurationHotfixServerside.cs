using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("broadcast_text_duration")]
    public sealed record BroadcastTextDurationHotfixServerside : IDataModel
    {
        [DBFieldName("ID", false, true)]
        public string ID;

        [DBFieldName("Locale", true)]
        public int? Locale;

        [DBFieldName("Duration")]
        public int? Duration;

        [DBFieldName("BroadcastTextID", true)]
        public uint? BroadcastTextID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = -ClientVersion.BuildInt;
    }
}
