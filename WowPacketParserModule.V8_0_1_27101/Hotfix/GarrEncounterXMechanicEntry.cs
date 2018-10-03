using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrEncounterXMechanic, HasIndexInData = false)]
    public class GarrEncounterXMechanicEntry
    {
        public byte GarrMechanicID { get; set; }
        public byte GarrMechanicSetID { get; set; }
        public ushort GarrEncounterID { get; set; }
    }
}
