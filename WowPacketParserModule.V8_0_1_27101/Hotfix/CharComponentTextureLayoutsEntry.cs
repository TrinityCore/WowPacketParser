using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharComponentTextureLayouts, HasIndexInData = false)]
    public class CharComponentTextureLayoutsEntry
    {
        public short Width { get; set; }
        public short Height { get; set; }
    }
}
