using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PathNodeProperty)]
    public class PathNodePropertyEntry
    {
        public int ID { get; set; }
        public ushort PathID { get; set; }
        public ushort Sequence { get; set; }
        public byte PropertyIndex { get; set; }
        public int Value { get; set; }
    }
}
