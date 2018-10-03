using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrAbilityCategory, HasIndexInData = false)]
    public class GarrAbilityCategoryEntry
    {
        public string Name { get; set; }
    }
}
