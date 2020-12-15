using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ModifierTree, HasIndexInData = false)]
    public class ModifierTreeEntry
    {
        public uint Parent { get; set; }
        public sbyte Operator { get; set; }
        public sbyte Amount { get; set; }
        public int Type { get; set; }
        public int Asset { get; set; }
        public int SecondaryAsset { get; set; }
        public sbyte TertiaryAsset { get; set; }
    }
}
