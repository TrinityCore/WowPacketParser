using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDisplayInfoExtra, HasIndexInData = false)]
    public class CreatureDisplayInfoExtraEntry
    {
        public sbyte DisplayRaceID { get; set; }
        public sbyte DisplaySexID { get; set; }
        public sbyte DisplayClassID { get; set; }
        public sbyte SkinID { get; set; }
        public sbyte FaceID { get; set; }
        public sbyte HairStyleID { get; set; }
        public sbyte HairColorID { get; set; }
        public sbyte FacialHairID { get; set; }
        public sbyte Flags { get; set; }
        public int BakeMaterialResourcesID { get; set; }
        public int HDBakeMaterialResourcesID { get; set; }
        [HotfixArray(3)]
        public byte[] CustomDisplayOption { get; set; }
    }
}
