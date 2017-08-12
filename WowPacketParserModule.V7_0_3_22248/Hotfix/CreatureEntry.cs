using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Creature, HasIndexInData = false)]
    public class CreatureEntry
    {
        [HotfixArray(3)]
        public uint[] Item { get; set; }
        public uint Mount { get; set; }
        [HotfixArray(4)]
        public uint[] DisplayID { get; set; }
        [HotfixArray(4)]
        public float[] DisplayIdProbability { get; set; }
        public string Name { get; set; }
        public string FemaleName { get; set; }
        public string SubName { get; set; }
        public string FemaleSubName { get; set; }
        public byte Type { get; set; }
        public byte Family { get; set; }
        public byte Classification { get; set; }
        public byte InhabitType { get; set; }
    }
}
