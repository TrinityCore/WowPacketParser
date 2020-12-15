using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemRandomProperties, HasIndexInData = false)]
    public class ItemRandomPropertiesEntry
    {
        public string Name { get; set; }
        [HotfixArray(5)]
        public ushort[] Enchantment { get; set; }
    }
}
