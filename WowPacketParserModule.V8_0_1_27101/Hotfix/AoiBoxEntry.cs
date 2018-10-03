using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AoiBox, HasIndexInData = false)]
    public class AoiBoxEntry
    {
        [HotfixArray(6)]
        public float[] Bounds { get; set; }
        public int Flags { get; set; }
        public int EncounterID { get; set; }
        public int WorldStateID { get; set; }
        public int MapID { get; set; }
    }
}
