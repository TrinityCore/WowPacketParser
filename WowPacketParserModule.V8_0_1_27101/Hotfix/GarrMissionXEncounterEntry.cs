using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrMissionXEncounter)]
    public class GarrMissionXEncounterEntry
    {
        public int ID { get; set; }
        public uint GarrEncounterID { get; set; }
        public uint GarrEncounterSetID { get; set; }
        public byte OrderIndex { get; set; }
        public int GarrMissionID { get; set; }
    }
}
