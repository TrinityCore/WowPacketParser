using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("num_talents_at_level")]
    public sealed record NumTalentsAtLevelHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("NumTalents")]
        public int? NumTalents;

        [DBFieldName("NumTalentsDeathKnight")]
        public int? NumTalentsDeathKnight;

        [DBFieldName("NumTalentsDemonHunter")]
        public int? NumTalentsDemonHunter;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("num_talents_at_level")]
    public sealed record NumTalentsAtLevelHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("NumTalents")]
        public int? NumTalents;

        [DBFieldName("NumTalentsDeathKnight")]
        public int? NumTalentsDeathKnight;

        [DBFieldName("NumTalentsDemonHunter")]
        public int? NumTalentsDemonHunter;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
