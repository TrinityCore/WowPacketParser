using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharHairGeosets, HasIndexInData = false)]
    public class CharHairGeosetsEntry
    {
        public byte RaceID { get; set; }
        public byte SexID { get; set; }
        public sbyte VariationID { get; set; }
        public byte GeosetID { get; set; }
        public byte Showscalp { get; set; }
        public sbyte VariationType { get; set; }
        public sbyte GeosetType { get; set; }
        public sbyte ColorIndex { get; set; }
        public int CustomGeoFileDataID { get; set; }
        public int HdCustomGeoFileDataID { get; set; }
    }
}
