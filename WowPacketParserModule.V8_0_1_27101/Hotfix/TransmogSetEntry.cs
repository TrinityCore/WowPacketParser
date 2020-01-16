using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
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
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public byte Unknown810 { get; set; }
        public byte ExpansionID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public int PatchID { get; set; }
        public short UiOrder { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_2_5_31921, false)]
        public int PlayerConditionID { get; set; }
    }
}
