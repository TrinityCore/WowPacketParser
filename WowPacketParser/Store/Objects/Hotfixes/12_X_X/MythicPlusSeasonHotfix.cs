using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("mythic_plus_season")]
    public sealed record MythicPlusSeasonHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MilestoneSeason")]
        public int? MilestoneSeason;

        [DBFieldName("StartTimeEvent")]
        public int? StartTimeEvent;

        [DBFieldName("ExpansionLevel")]
        public int? ExpansionLevel;

        [DBFieldName("HeroicLFGDungeonMinGear")]
        public int? HeroicLFGDungeonMinGear;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
