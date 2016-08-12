using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemEffect, HasIndexInData = false)]
    public class ItemEffectEntry
    {
        public uint ItemID { get; set; }
        public uint SpellID { get; set; }
        public int Cooldown { get; set; }
        public int CategoryCooldown { get; set; }
        public short Charges { get; set; }
        public ushort Category { get; set; }
        public ushort ChrSpecializationID { get; set; }
        public byte OrderIndex { get; set; }
        public byte Trigger { get; set; }
    }
}