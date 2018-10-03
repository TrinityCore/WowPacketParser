using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDisplayInfoCond, HasIndexInData = false)]
    public class CreatureDisplayInfoCondEntry
    {
        public ulong RaceMask { get; set; }
        public sbyte OrderIndex { get; set; }
        public sbyte Gender { get; set; }
        public uint ClassMask { get; set; }
        public uint SkinColorMask { get; set; }
        public uint HairColorMask { get; set; }
        public uint HairStyleMask { get; set; }
        public uint FaceStyleMask { get; set; }
        public uint FacialHairStyleMask { get; set; }
        public int CreatureModelDataID { get; set; }
        [HotfixArray(2)]
        public int[] CustomOption0Mask { get; set; }
        [HotfixArray(2)]
        public int[] CustomOption1Mask { get; set; }
        [HotfixArray(2)]
        public int[] CustomOption2Mask { get; set; }
        [HotfixArray(3)]
        public int[] TextureVariationFileDataID { get; set; }
        public int CreatureDisplayInfoID { get; set; }
    }
}
