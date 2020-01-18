using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_2_0_30898.Hotfix
{
    [HotfixStructure(DB2Hash.ModifierTree, ClientVersionBuild.V8_2_0_30898, HasIndexInData = false)]
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
