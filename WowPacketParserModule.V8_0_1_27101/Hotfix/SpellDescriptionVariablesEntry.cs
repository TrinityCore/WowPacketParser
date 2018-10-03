using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellDescriptionVariables, HasIndexInData = false)]
    public class SpellDescriptionVariablesEntry
    {
        public string Variables { get; set; }
    }
}
