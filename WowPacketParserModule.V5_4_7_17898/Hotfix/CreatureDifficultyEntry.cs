using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_4_7_17898.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDifficulty)]
    public class CreatureDifficultyEntry
    {
        public int ID { get; set; }
        public int CreatureID { get; set; }
        public int FactionID { get; set; }
        public int Expansion { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        [HotfixArray(5)]
        public int[] Flags { get; set; }
    }
}
