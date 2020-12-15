using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.RandPropPoints, HasIndexInData = false)]
    public class RandPropPointsEntry
    {
        public int DamageReplaceStat { get; set; }
        public int DamageSecondary { get; set; }
        [HotfixArray(5)]
        public uint[] Epic { get; set; }
        [HotfixArray(5)]
        public uint[] Superior { get; set; }
        [HotfixArray(5)]
        public uint[] Good { get; set; }
    }
}
