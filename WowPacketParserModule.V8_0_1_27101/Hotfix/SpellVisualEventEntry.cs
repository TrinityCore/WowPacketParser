using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualEvent, HasIndexInData = false)]
    public class SpellVisualEventEntry
    {
        public int StartEvent { get; set; }
        public int EndEvent { get; set; }
        public int StartMinOffsetMs { get; set; }
        public int StartMaxOffsetMs { get; set; }
        public int EndMinOffsetMs { get; set; }
        public int EndMaxOffsetMs { get; set; }
        public int TargetType { get; set; }
        public int SpellVisualKitID { get; set; }
        public int SpellVisualID { get; set; }
    }
}
