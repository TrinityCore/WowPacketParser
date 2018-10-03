using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.VirtualAttachmentCustomization, HasIndexInData = false)]
    public class VirtualAttachmentCustomizationEntry
    {
        public short VirtualAttachmentID { get; set; }
        public int FileDataID { get; set; }
        public short PositionerID { get; set; }
    }
}
