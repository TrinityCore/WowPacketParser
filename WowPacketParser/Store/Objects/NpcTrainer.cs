using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("npc_trainer")]
    public sealed record NpcTrainer : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID", true)]
        public int? SpellID;

        [DBFieldName("MoneyCost")]
        public uint? MoneyCost;

        [DBFieldName("ReqSkillLine")]
        public uint? ReqSkillLine;

        [DBFieldName("ReqSkillRank")]
        public uint? ReqSkillRank;

        [DBFieldName("ReqLevel")]
        public uint? ReqLevel;
    }
}
