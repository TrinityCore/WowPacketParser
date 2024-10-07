using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("skill_race_class_info")]
    public sealed record SkillRaceClassInfoHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("SkillID")]
        public ushort? SkillID;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Availability")]
        public int? Availability;

        [DBFieldName("MinLevel")]
        public sbyte? MinLevel;

        [DBFieldName("SkillTierID")]
        public short? SkillTierID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
