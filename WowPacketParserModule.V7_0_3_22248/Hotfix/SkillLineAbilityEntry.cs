using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SkillLineAbility, ClientVersionBuild.V7_0_3_22248, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class SkillLineAbilityEntry
    {
        public uint SpellID { get; set; }
        public uint RaceMask { get; set; }
        public uint SupercedesSpell { get; set; }
        public uint Unknown703 { get; set; }
        public ushort SkillLine { get; set; }
        public ushort MinSkillLineRank { get; set; }
        public ushort TrivialSkillLineRankHigh { get; set; }
        public ushort TrivialSkillLineRankLow { get; set; }
        public ushort UniqueBit { get; set; }
        public ushort TradeSkillCategoryID { get; set; }
        public byte AcquireMethod { get; set; }
        public byte NumSkillUps { get; set; }
        public uint ClassMask { get; set; }
    }
}