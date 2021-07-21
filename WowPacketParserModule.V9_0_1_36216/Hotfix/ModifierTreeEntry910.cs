using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.ModifierTree, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class ModifierTreeEntry
    {
        public uint Parent { get; set; }
        public sbyte Operator { get; set; }
        public sbyte Amount { get; set; }
        public int Type { get; set; }
        public int Asset { get; set; }
        public int SecondaryAsset { get; set; }
        public int TertiaryAsset { get; set; }
    }
}
