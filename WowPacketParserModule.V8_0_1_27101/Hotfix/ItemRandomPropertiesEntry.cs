using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemRandomProperties, HasIndexInData = false)]
    public class ItemRandomPropertiesEntry
    {
        public string Name { get; set; }
        [HotfixArray(5)]
        public ushort[] Enchantment { get; set; }
    }
}
