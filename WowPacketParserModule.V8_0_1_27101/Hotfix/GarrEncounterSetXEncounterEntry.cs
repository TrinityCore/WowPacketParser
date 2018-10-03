using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrEncounterSetXEncounter)]
    public class GarrEncounterSetXEncounterEntry
    {
        public int ID { get; set; }
        public uint GarrEncounterID { get; set; }
        public uint GarrEncounterSetID { get; set; }
    }
}
