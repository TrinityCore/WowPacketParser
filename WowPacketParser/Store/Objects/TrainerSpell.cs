using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("trainer_spell")]
    public sealed record TrainerSpell : IDataModel
    {
        public uint[] ReqAbility;

        public void ConvertToDBStruct()
        {
            ReqAbility1 = ReqAbility[0];
            ReqAbility2 = ReqAbility[1];
            ReqAbility3 = ReqAbility[2];
        }

        [DBFieldName("TrainerId", true)]
        public uint? TrainerId;

        [DBFieldName("SpellId", true)]
        public uint? SpellId;

        [DBFieldName("MoneyCost")]
        public uint? MoneyCost;

        [DBFieldName("ReqSkillLine")]
        public uint? ReqSkillLine;

        [DBFieldName("ReqSkillRank")]
        public uint? ReqSkillRank;

        [DBFieldName("ReqAbility1")]
        public uint? ReqAbility1;

        [DBFieldName("ReqAbility2")]
        public uint? ReqAbility2;

        [DBFieldName("ReqAbility3")]
        public uint? ReqAbility3;

        [DBFieldName("ReqLevel")]
        public byte? ReqLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public string FactionHelper;
    }
}
