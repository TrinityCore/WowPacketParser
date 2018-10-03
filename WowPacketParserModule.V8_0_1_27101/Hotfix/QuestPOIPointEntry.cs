using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.QuestPOIPoint)]
    public class QuestPOIPointEntry
    {
        public int ID { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public int QuestPOIBlobID { get; set; }
    }
}
