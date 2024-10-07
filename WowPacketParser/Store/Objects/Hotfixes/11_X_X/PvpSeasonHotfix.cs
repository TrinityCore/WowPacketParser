using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("pvp_season")]
    public sealed record PvpSeasonHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MilestoneSeason")]
        public int? MilestoneSeason;

        [DBFieldName("AllianceAchievementID")]
        public int? AllianceAchievementID;

        [DBFieldName("HordeAchievementID")]
        public int? HordeAchievementID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
