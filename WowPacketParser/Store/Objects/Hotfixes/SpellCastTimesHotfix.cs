using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_cast_times")]
    public sealed record SpellCastTimesHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Base")]
        public int? Base;

        [DBFieldName("Minimum")]
        public int? Minimum;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_cast_times")]
    public sealed record SpellCastTimesHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Base")]
        public int? Base;

        [DBFieldName("PerLevel")]
        public short? PerLevel;

        [DBFieldName("Minimum")]
        public int? Minimum;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
