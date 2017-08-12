using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Lock, HasIndexInData = false)]
    public class LockEntry
    {
        [HotfixArray(8)]
        public uint[] Index { get; set; }
        [HotfixArray(8)]
        public ushort[] Skill { get; set; }
        [HotfixArray(8)]
        public byte[] Type { get; set; }
        [HotfixArray(8)]
        public byte[] Action { get; set; }
    }
}