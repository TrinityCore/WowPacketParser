using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogSet)]
    public class TransmogSetEntry
    {
        public string Name { get; set; }
        public ushort BaseSetID { get; set; }
        public ushort UIOrder { get; set; }
        public byte ExpansionID { get; set; }
        public uint ID { get; set; }
        public uint Flags { get; set; }
        public uint Unknown { get; set; }
        public uint ClassMask { get; set; }
        public uint ItemNameDescriptionID { get; set; }
        public uint TransmogSetGroupID { get; set; }
    }
}
