using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ScheduledWorldState, HasIndexInData = false)]
    public class ScheduledWorldStateEntry
    {
        public int ScheduledWorldStateGroupID { get; set; }
        public int WorldStateID { get; set; }
        public int Value { get; set; }
        public int DurationSecs { get; set; }
        public int Weight { get; set; }
        public int UniqueCategory { get; set; }
        public int Flags { get; set; }
        public int OrderIndex { get; set; }
    }
}
