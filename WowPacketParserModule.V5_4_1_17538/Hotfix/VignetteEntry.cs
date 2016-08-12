using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_4_1_17538.Hotfix
{
    [HotfixStructure(DB2Hash.Vignette)]
    public class VignetteEntry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Icon { get; set; }
        public int Flag { get; set; }
        public float UnkFloat1 { get; set; }
        public float UnkFloat2 { get; set; }
    }
}
