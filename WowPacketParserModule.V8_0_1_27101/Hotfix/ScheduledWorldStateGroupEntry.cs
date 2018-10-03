using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ScheduledWorldStateGroup, HasIndexInData = false)]
    public class ScheduledWorldStateGroupEntry
    {
        public int Flags { get; set; }
        public int ScheduledIntervalID { get; set; }
        public int SelectionType { get; set; }
        public int SelectionCount { get; set; }
        public int Priority { get; set; }
    }
}
