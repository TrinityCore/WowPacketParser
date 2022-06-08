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

        [DBFieldName("Gender", TargetedDatabase.Classic, TargetedDatabase.WarlordsOfDraenor)]
        public Gender? Gender;

        [DBFieldName("DisplayID_Other_Gender")]
        public uint? DisplayIDOtherGender = 0;

        [DBFieldName("VerifiedBuild", TargetedDatabase.Legion)]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
