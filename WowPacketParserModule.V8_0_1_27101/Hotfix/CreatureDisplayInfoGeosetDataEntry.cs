using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDisplayInfoGeosetData, HasIndexInData = false)]
    public class CreatureDisplayInfoGeosetDataEntry
    {
        public byte GeosetIndex { get; set; }
        public byte GeosetValue { get; set; }
        public uint CreatureDisplayInfoID { get; set; }
    }
}
