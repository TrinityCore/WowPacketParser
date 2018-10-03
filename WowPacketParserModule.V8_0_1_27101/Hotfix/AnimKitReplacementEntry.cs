using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AnimKitReplacement)]
    public class AnimKitReplacementEntry
    {
        public int ID { get; set; }
        public ushort SrcAnimKitID { get; set; }
        public ushort DstAnimKitID { get; set; }
        public ushort Flags { get; set; }
        public ushort ParentAnimReplacementSetID { get; set; }
    }
}
