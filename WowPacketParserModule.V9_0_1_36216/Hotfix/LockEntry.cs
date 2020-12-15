using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.Lock, HasIndexInData = false)]
    public class LockEntry
    {
        public int Flags { get; set; }
        [HotfixArray(8)]
        public int[] Index { get; set; }
        [HotfixArray(8)]
        public ushort[] Skill { get; set; }
        [HotfixArray(8)]
        public byte[] Type { get; set; }
        [HotfixArray(8)]
        public byte[] Action { get; set; }
    }
}
