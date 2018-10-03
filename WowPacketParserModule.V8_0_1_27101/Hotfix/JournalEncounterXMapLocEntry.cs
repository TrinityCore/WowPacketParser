using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.JournalEncounterXMapLoc, HasIndexInData = false)]
    public class JournalEncounterXMapLocEntry
    {
        [HotfixArray(2)]
        public float[] Map { get; set; }
        public int JournalEncounterID { get; set; }
        public int MapDisplayConditionID { get; set; }
        public byte Flags { get; set; }
        public int UiMapID { get; set; }
    }
}
