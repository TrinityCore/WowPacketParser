using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Spell, HasIndexInData = false)]
    public class SpellEntry
    {
        public string NameSubtext { get; set; }
        public string Description { get; set; }
        public string AuraDescription { get; set; }
    }
}
