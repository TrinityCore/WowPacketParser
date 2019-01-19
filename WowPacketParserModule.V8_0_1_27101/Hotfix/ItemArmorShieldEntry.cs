using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemArmorShield, HasIndexInData = false)]
    public class ItemArmorShieldEntry
    {
        [HotfixArray(7)]
        public float[] Quality { get; set; }
        public ushort ItemLevel { get; set; }
    }
}
