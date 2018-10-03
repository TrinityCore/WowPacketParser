using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharComponentTextureSections, HasIndexInData = false)]
    public class CharComponentTextureSectionsEntry
    {
        public sbyte CharComponentTextureLayoutID { get; set; }
        public sbyte SectionType { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public short Width { get; set; }
        public short Height { get; set; }
        public uint OverlapSectionMask { get; set; }
    }
}
