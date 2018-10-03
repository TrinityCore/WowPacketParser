using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PageTextMaterial, HasIndexInData = false)]
    public class PageTextMaterialEntry
    {
        public string Name { get; set; }
    }
}
