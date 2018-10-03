using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrBuildingDoodadSet, HasIndexInData = false)]
    public class GarrBuildingDoodadSetEntry
    {
        public byte GarrBuildingID { get; set; }
        public byte Type { get; set; }
        public byte HordeDoodadSet { get; set; }
        public byte AllianceDoodadSet { get; set; }
        public byte SpecializationID { get; set; }
    }
}
