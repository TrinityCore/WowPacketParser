using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GlyphExclusiveCategory, HasIndexInData = false)]
    public class GlyphExclusiveCategoryEntry
    {
        public string Name { get; set; }
    }
}
