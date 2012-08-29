using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_model_info")]
    public class ModelData
    {
        [DBFieldName("bounding_radius")]
        public float BoundingRadius;

        [DBFieldName("combat_reach")]
        public float CombatReach;

        [DBFieldName("gender")]
        public Gender Gender;

        //[DBFieldName("modelid_other_gender")]
        //public uint ModelIdOtherGender;
    }
}
