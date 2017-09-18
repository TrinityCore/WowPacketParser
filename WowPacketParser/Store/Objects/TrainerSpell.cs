using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("trainer_spell")]
    public sealed class TrainerSpell : IDataModel
    {
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

        [DBFieldName("ReqAbility", 3)]
        public uint[] ReqAbility;

        [DBFieldName("ReqLevel")]
        public byte? ReqLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
