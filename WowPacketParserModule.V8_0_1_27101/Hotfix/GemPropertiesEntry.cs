using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GemProperties, HasIndexInData = false)]
    public class GemPropertiesEntry
    {
        public ushort EnchantID { get; set; }
        public int Type { get; set; }
        public ushort MinItemLevel { get; set; }
    }
}
