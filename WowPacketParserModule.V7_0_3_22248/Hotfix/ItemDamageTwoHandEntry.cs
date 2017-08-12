using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemDamageTwoHand, HasIndexInData = false)]
    public class ItemDamageTwoHandEntry
    {
        [HotfixArray(7)]
        public float[] DPS { get; set; }
        public ushort ItemLevel { get; set; }
    }
}