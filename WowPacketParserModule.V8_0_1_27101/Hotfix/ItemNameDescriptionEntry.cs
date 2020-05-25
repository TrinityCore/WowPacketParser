using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemNameDescription, HasIndexInData = false)]
    public class ItemNameDescriptionEntry
    {
        public string Description { get; set; }
        public int Color { get; set; }
    }
}
