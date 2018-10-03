using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AnimKitPriority, HasIndexInData = false)]
    public class AnimKitPriorityEntry
    {
        public byte Priority { get; set; }
    }
}
