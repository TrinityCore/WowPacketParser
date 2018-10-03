using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemEffect, HasIndexInData = false)]
    public class ItemEffectEntry
    {
        public byte LegacySlotIndex { get; set; }
        public sbyte TriggerType { get; set; }
        public short Charges { get; set; }
        public int CoolDownMSec { get; set; }
        public int CategoryCoolDownMSec { get; set; }
        public ushort SpellCategoryID { get; set; }
        public int SpellID { get; set; }
        public ushort ChrSpecializationID { get; set; }
        public int ParentItemID { get; set; }
    }
}
