using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellFlyoutItem, HasIndexInData = false)]
    public class SpellFlyoutItemEntry
    {
        public int SpellID { get; set; }
        public byte Slot { get; set; }
        public byte SpellFlyoutID { get; set; }
    }
}
