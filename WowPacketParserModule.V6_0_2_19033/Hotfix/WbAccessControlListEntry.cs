using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.WbAccessControlList)]
    public class WbAccessControlListEntry
    {
        public uint Id { get; set; }
        public string Address { get; set; }
        public uint UnkMoP1 { get; set; }
        public uint UnkMoP2 { get; set; }
        public uint UnkMoP3 { get; set; }
        public uint UnkMoP4 { get; set; }
    }
}