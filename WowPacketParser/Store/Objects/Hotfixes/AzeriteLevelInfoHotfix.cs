using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("azerite_level_info")]
    public sealed record AzeriteLevelInfoHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("BaseExperienceToNextLevel")]
        public ulong? BaseExperienceToNextLevel;

        [DBFieldName("MinimumExperienceToNextLevel")]
        public ulong? MinimumExperienceToNextLevel;

        [DBFieldName("ItemLevel")]
        public int? ItemLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("azerite_level_info")]
    public sealed record AzeriteLevelInfoHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("BaseExperienceToNextLevel")]
        public ulong? BaseExperienceToNextLevel;

        [DBFieldName("MinimumExperienceToNextLevel")]
        public ulong? MinimumExperienceToNextLevel;

        [DBFieldName("ItemLevel")]
        public int? ItemLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
