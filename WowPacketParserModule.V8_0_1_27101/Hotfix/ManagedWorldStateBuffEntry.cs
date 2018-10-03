using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ManagedWorldStateBuff, HasIndexInData = false)]
    public class ManagedWorldStateBuffEntry
    {
        public int BuffSpellID { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint OccurrenceValue { get; set; }
        public int ManagedWorldStateID { get; set; }
    }
}
