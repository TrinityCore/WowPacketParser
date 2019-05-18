using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellName, HasIndexInData = false)]
    public class SpellNameEntry
    {
        public string Name { get; set; }
    }
}
