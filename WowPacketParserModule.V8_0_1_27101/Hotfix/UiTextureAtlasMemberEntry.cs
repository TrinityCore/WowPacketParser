using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiTextureAtlasMember)]
    public class UiTextureAtlasMemberEntry
    {
        public string CommittedName { get; set; }
        public int ID { get; set; }
        public ushort UiTextureAtlasID { get; set; }
        public short CommittedLeft { get; set; }
        public short CommittedRight { get; set; }
        public short CommittedTop { get; set; }
        public short CommittedBottom { get; set; }
        public ushort UiTextureAtlasElementID { get; set; }
        public sbyte CommittedFlags { get; set; }
        public byte UiCanvasID { get; set; }
    }
}
