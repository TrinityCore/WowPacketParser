using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_4_8_18291.Hotfix
{
    [HotfixStructure(DB2Hash.Creature)]
    public class CreatureEntry
    {
        public int ID { get; set; }
        [HotfixArray(3)]
        public int[] Item { get; set; }
        public uint Mount { get; set; }
        [HotfixArray(4)]
        public int[] DisplayID { get; set; }
        [HotfixArray(4)]
        public int[] DisplayIDProbability { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string FemaleSubName { get; set; }
        public int Rank { get; set; }
        public int InhabitType { get; set; }
    }
}
