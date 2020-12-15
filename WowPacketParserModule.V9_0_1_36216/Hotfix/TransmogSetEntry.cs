using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogSet)]
    public class TransmogSetEntry
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public int ClassMask { get; set; }
        public uint TrackingQuestID { get; set; }
        public int Flags { get; set; }
        public uint TransmogSetGroupID { get; set; }
        public int ItemNameDescriptionID { get; set; }
        public ushort ParentTransmogSetID { get; set; }
        public byte Unknown810 { get; set; }
        public byte ExpansionID { get; set; }
        public int PatchID { get; set; }
        public short UiOrder { get; set; }
        public int PlayerConditionID { get; set; }
    }
}
