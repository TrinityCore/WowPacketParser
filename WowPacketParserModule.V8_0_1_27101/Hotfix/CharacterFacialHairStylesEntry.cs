using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharacterFacialHairStyles, HasIndexInData = false)]
    public class CharacterFacialHairStylesEntry
    {
        [HotfixArray(5)]
        public int[] Geoset { get; set; }
        public byte RaceID { get; set; }
        public byte SexID { get; set; }
        public byte VariationID { get; set; }
    }
}
