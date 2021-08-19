using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.PathNode)]
    public class PathNodeEntry
    {
        public uint ID { get; set; }
        public ushort PathId { get; set; }
        public ushort Idx { get; set; }
    }
}
