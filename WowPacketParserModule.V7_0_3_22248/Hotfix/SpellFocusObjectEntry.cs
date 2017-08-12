using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellFocusObject, HasIndexInData = false)]
    public class SpellFocusObjectEntry
    {
        public string Name { get; set; }
    }
}