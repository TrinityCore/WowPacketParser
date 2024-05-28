using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("num_talents_at_level")]
    public sealed record NumTalentsAtLevelHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("NumTalents")]
        public int? NumTalents;

        [DBFieldName("NumTalentsDeathKnight")]
        public int? NumTalentsDeathKnight;

        [DBFieldName("NumTalentsDemonHunter")]
        public int? NumTalentsDemonHunter;

        [DBFieldName("NumberOfTalents")]
        public float? NumberOfTalents;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
