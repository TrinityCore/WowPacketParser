using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ModifierTree, HasIndexInData = false)]
    public class ModifierTreeEntry
    {
        [HotfixArray(2)]
        public uint[] Asset { get; set; }
        public ushort Parent { get; set; }
        public byte Type { get; set; }
        public byte Unk700 { get; set; }
        public byte Operator { get; set; }
        public byte Amount { get; set; }
    }
}