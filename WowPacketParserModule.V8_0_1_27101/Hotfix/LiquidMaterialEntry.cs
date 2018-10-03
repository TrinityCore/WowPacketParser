using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LiquidMaterial, HasIndexInData = false)]
    public class LiquidMaterialEntry
    {
        public sbyte Flags { get; set; }
        public sbyte LVF { get; set; }
    }
}
