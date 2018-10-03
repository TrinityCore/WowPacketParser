using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PaperDollItemFrame, HasIndexInData = false)]
    public class PaperDollItemFrameEntry
    {
        public string ItemButtonName { get; set; }
        public int SlotIconFileID { get; set; }
        public byte SlotNumber { get; set; }
    }
}
