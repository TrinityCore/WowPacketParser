using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_model_info")]
    public sealed record ModelData : IDataModel
    {
        [DBFieldName("DisplayID", true)]
        public uint? DisplayID;

        [DBFieldName("BoundingRadius")]
        public float? BoundingRadius;

        [DBFieldName("CombatReach")]
        public float? CombatReach;

        [DBFieldName("Gender", TargetedDatabaseFlag.TillCataclysm | TargetedDatabaseFlag.Classic)]
        public Gender? Gender;

        [DBFieldName("DisplayID_Other_Gender")]
        public uint? DisplayIDOtherGender = 0;

        [DBFieldName("VerifiedBuild", TargetedDatabaseFlag.SinceLegion | TargetedDatabaseFlag.WotlkClassic)]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
