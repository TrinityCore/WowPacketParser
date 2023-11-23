using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_duration")]
    public sealed record SpellDurationHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Duration")]
        public int? Duration;

        [DBFieldName("MaxDuration")]
        public int? MaxDuration;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_duration")]
    public sealed record SpellDurationHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Duration")]
        public int? Duration;

        [DBFieldName("DurationPerLevel")]
        public uint? DurationPerLevel;

        [DBFieldName("MaxDuration")]
        public int? MaxDuration;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
