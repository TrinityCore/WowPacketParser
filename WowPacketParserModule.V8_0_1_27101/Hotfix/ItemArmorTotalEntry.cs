using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemArmorTotal, HasIndexInData = false)]
    public class ItemArmorTotalEntry
    {
        public short ItemLevel { get; set; }
        public float Cloth { get; set; }
        public float Leather { get; set; }
        public float Mail { get; set; }
        public float Plate { get; set; }
    }
}
