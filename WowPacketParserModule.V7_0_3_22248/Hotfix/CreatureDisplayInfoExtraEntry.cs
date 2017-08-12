using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDisplayInfoExtra, HasIndexInData = false)]
    public class CreatureDisplayInfoExtraEntry
    {
        public uint FileDataID { get; set; }
        public uint HDFileDataID { get; set; }
        public byte DisplayRaceID { get; set; }
        public byte DisplaySexID { get; set; }
        public byte DisplayClassID { get; set; }
        public byte SkinID { get; set; }
        public byte FaceID { get; set; }
        public byte HairStyleID { get; set; }
        public byte HairColorID { get; set; }
        public byte FacialHairID { get; set; }
        [HotfixArray(3)]
        public byte[] CustomDisplayOption { get; set; }
        public byte Flags { get; set; }
    }
}