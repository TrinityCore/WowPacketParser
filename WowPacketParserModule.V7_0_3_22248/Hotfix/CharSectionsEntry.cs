using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CharSections, HasIndexInData = false)]
    public class CharSectionsEntry
    {
        [HotfixArray(3)]
        public uint[] TextureFileDataID { get; set; }
        public ushort Flags { get; set; }
        public byte Race { get; set; }
        public byte Gender { get; set; }
        public byte GenType { get; set; }
        public byte Type { get; set; }
        public byte Color { get; set; }
    }
}