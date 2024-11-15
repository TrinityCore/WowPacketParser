using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("broadcast_text_duration")]
    public sealed record BroadcastTextDurationHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("BroadcastTextID")]
        public uint? BroadcastTextID;

        [DBFieldName("Locale")]
        public int? Locale;

        [DBFieldName("Duration")]
        public int? Duration;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("broadcast_text_duration")]
    public sealed record BroadcastTextDurationHotfix1105 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Locale")]
        public int? Locale;

        [DBFieldName("Duration")]
        public int? Duration;

        [DBFieldName("BroadcastTextID")]
        public uint? BroadcastTextID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
