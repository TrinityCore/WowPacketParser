using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PathProperty)]
    public class PathPropertyEntry
    {
        public int ID { get; set; }
        public ushort PathID { get; set; }
        public byte PropertyIndex { get; set; }
        public int Value { get; set; }
    }
}
