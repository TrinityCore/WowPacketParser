using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrSpecialization, HasIndexInData = false)]
    public class GarrSpecializationEntry
    {
        public string Name { get; set; }
        public string Tooltip { get; set; }
        public byte BuildingType { get; set; }
        public byte SpecType { get; set; }
        public byte RequiredUpgradeLevel { get; set; }
        public int IconFileDataID { get; set; }
        [HotfixArray(2)]
        public float[] Param { get; set; }
    }
}
