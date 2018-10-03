using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MultiTransitionProperties, HasIndexInData = false)]
    public class MultiTransitionPropertiesEntry
    {
        public uint TransitionType { get; set; }
        public uint DurationMS { get; set; }
        public uint Flags { get; set; }
        public int StartSpellVisualKitID { get; set; }
        public int EndSpellVisualKitID { get; set; }
    }
}
