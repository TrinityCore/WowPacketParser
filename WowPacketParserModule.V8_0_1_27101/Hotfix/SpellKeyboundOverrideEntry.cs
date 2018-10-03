using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellKeyboundOverride, HasIndexInData = false)]
    public class SpellKeyboundOverrideEntry
    {
        public string Function { get; set; }
        public sbyte Type { get; set; }
        public int Data { get; set; }
    }
}
