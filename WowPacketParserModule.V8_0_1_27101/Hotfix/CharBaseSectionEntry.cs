using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharBaseSection, HasIndexInData = false)]
    public class CharBaseSectionEntry
    {
        public byte LayoutResType { get; set; }
        public byte VariationEnum { get; set; }
        public byte ResolutionVariationEnum { get; set; }
    }
}
