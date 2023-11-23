using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("mythic_plus_season")]
    public sealed record MythicPlusSeasonHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MilestoneSeason")]
        public int? MilestoneSeason;

        [DBFieldName("ExpansionLevel")]
        public int? ExpansionLevel;

        [DBFieldName("HeroicLFGDungeonMinGear")]
        public int? HeroicLFGDungeonMinGear;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("mythic_plus_season")]
    public sealed record MythicPlusSeasonHotfix342: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MilestoneSeason")]
        public int? MilestoneSeason;

        [DBFieldName("ExpansionLevel")]
        public int? ExpansionLevel;

        [DBFieldName("HeroicLFGDungeonMinGear")]
        public int? HeroicLFGDungeonMinGear;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
