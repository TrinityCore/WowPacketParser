using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("skill_race_class_info")]
    public sealed record SkillRaceClassInfoHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("SkillID")]
        public short? SkillID;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("Availability")]
        public sbyte? Availability;

        [DBFieldName("MinLevel")]
        public sbyte? MinLevel;

        [DBFieldName("SkillTierID")]
        public short? SkillTierID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("skill_race_class_info")]
    public sealed record SkillRaceClassInfoHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("SkillID")]
        public short? SkillID;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("Availability")]
        public sbyte? Availability;

        [DBFieldName("MinLevel")]
        public sbyte? MinLevel;

        [DBFieldName("SkillTierID")]
        public short? SkillTierID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
