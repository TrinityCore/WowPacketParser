using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MapCelestialBody, HasIndexInData = false)]
    public class MapCelestialBodyEntry
    {
        public short CelestialBodyID { get; set; }
        public uint PlayerConditionID { get; set; }
        public short MapID { get; set; }
    }
}
