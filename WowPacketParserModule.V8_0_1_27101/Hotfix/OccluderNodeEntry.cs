using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.OccluderNode)]
    public class OccluderNodeEntry
    {
        public int ID { get; set; }
        public ushort PathID { get; set; }
        public short Sequence { get; set; }
        public int LocationID { get; set; }
    }
}
