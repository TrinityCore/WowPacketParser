using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_model_info")]
    public class ModelData
    {
        [DBFieldName("BoundingRadius")]
        public float BoundingRadius;

        [DBFieldName("BoundingRadius")]
        public float CombatReach;

        [DBFieldName("DisplayID_Other_Gender")]
        public Gender Gender;

        //[DBFieldName("modelid_other_gender")]
        //public uint ModelIdOtherGender;
    }
}
