using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ScheduledInterval, HasIndexInData = false)]
    public class ScheduledIntervalEntry
    {
        public int Flags { get; set; }
        public int RepeatType { get; set; }
        public int DurationSecs { get; set; }
        public int OffsetSecs { get; set; }
        public int DateAlignmentType { get; set; }
    }
}
