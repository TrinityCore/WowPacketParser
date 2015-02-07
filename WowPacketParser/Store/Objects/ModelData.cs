using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_model_info")]
    public class ModelData
    {
        [DBFieldName("BoundingRadius")]
        public float BoundingRadius;

        [DBFieldName("CombatReach")]
        public float CombatReach;

        [DBFieldName("gender", ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public Gender Gender;

        //[DBFieldName("modelid_other_gender")]
        //public uint ModelIdOtherGender;
    }
}
