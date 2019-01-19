using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemBagFamily, HasIndexInData = false)]
    public class ItemBagFamilyEntry
    {
        public string Name { get; set; }
    }
}
