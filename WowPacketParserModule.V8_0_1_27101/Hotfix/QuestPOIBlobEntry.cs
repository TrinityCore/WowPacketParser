using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.QuestPOIBlob)]
    public class QuestPOIBlobEntry
    {
        public int ID { get; set; }
        public short MapID { get; set; }
        public int UiMapID { get; set; }
        public byte NumPoints { get; set; }
        public uint QuestID { get; set; }
        public int ObjectiveIndex { get; set; }
        public uint PlayerConditionID { get; set; }
    }
}
