using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemArmorTotal, HasIndexInData = false)]
    public class ItemArmorTotalEntry
    {
        [HotfixArray(4)]
        public float[] Value { get; set; }
        public ushort ItemLevel { get; set; }
    }
}