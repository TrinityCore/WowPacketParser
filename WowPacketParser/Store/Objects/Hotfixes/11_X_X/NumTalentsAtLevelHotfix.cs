using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("num_talents_at_level")]
    public sealed record NumTalentsAtLevelHotfix1100: IDataModel
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
    public sealed record NumTalentsAtLevelHotfix1115 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("NumTalents")]
        public int? NumTalents;

        [DBFieldName("NumTalentsDeathKnight")]
        public int? NumTalentsDeathKnight;

        [DBFieldName("NumTalentsDemonHunter")]
        public int? NumTalentsDemonHunter;

        [DBFieldName("Unknown1115")]
        public float? Unknown1115;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
