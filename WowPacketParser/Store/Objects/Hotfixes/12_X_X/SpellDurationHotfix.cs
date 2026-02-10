using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_duration")]
    public sealed record SpellDurationHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Duration")]
        public int? Duration;

        [DBFieldName("MaxDuration")]
        public int? MaxDuration;

        [DBFieldName("DurationPerResource")]
        public int? DurationPerResource;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
