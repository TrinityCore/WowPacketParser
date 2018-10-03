using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AreaTriggerActionSet, HasIndexInData = false)]
    public class AreaTriggerActionSetEntry
    {
        public ushort Flags { get; set; }
    }
}
