using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrClassSpecPlayerCond, HasIndexInData = false)]
    public class GarrClassSpecPlayerCondEntry
    {
        public string Name { get; set; }
        public uint GarrClassSpecID { get; set; }
        public uint PlayerConditionID { get; set; }
        public int IconFileDataID { get; set; }
        public int FlavorGarrStringID { get; set; }
        public byte OrderIndex { get; set; }
    }
}
