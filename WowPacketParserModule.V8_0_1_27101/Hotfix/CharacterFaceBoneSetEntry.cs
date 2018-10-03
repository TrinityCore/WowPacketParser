using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharacterFaceBoneSet, HasIndexInData = false)]
    public class CharacterFaceBoneSetEntry
    {
        public byte SexID { get; set; }
        public int ModelFileDataID { get; set; }
        public byte FaceVariationIndex { get; set; }
        public int BoneSetFileDataID { get; set; }
        public byte RaceID { get; set; }
    }
}
