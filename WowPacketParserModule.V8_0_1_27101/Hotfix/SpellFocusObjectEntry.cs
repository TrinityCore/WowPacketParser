using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellFocusObject, HasIndexInData = false)]
    public class SpellFocusObjectEntry
    {
        public string Name { get; set; }
    }
}
