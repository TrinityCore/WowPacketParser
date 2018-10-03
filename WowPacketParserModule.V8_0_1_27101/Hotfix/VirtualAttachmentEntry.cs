using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.VirtualAttachment, HasIndexInData = false)]
    public class VirtualAttachmentEntry
    {
        public string Name { get; set; }
        public short PositionerID { get; set; }
    }
}
