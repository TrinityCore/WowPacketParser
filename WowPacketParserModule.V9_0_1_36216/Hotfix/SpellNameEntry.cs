using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellName, HasIndexInData = false)]
    public class SpellNameEntry
    {
        public string Name { get; set; }
    }
}
