using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SkillLineAbility)]
    public class SkillLineAbilityEntry
    {
        public long RaceMask { get; set; }
        public uint ID { get; set; }
        public short SkillLine { get; set; }
        public int Spell { get; set; }
        public short MinSkillLineRank { get; set; }
        public int ClassMask { get; set; }
        public int SupercedesSpell { get; set; }
        public sbyte AcquireMethod { get; set; }
        public short TrivialSkillLineRankHigh { get; set; }
        public short TrivialSkillLineRankLow { get; set; }
        public int Flags { get; set; }
        public sbyte NumSkillUps { get; set; }
        public short UniqueBit { get; set; }
        public short TradeSkillCategoryID { get; set; }
        public short SkillupSkillLineID { get; set; }
    }
}
