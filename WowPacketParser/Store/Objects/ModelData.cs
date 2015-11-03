using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_model_info")]
    public sealed class ModelData : IDataModel
    {
        [DBFieldName("DisplayID", true)]
        public uint? DisplayID;

        [DBFieldName("BoundingRadius")]
        public float? BoundingRadius;

        [DBFieldName("CombatReach")]
        public float? CombatReach;

        [DBFieldName("Gender", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor)]
        public Gender? Gender;

        [DBFieldName("DisplayID_Other_Gender")]
        public uint? DisplayIDOtherGender;
    }
}
