using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemDamageOneHandCaster, HasIndexInData = false)]
    public class ItemDamageOneHandCasterEntry
    {
        public ushort ItemLevel { get; set; }
        [HotfixArray(7)]
        public float[] Quality { get; set; }
    }
}
