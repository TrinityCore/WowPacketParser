using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemPriceBase, HasIndexInData = false)]
    public class ItemPriceBaseEntry
    {
        public ushort ItemLevel { get; set; }
        public float Armor { get; set; }
        public float Weapon { get; set; }
    }
}
