using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_1_0_28724.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiPathNode, ClientVersionBuild.V8_1_0_28724)]
    public class TaxiPathNodeEntry
    {
        [HotfixArray(3, true)]
        public float[] Loc { get; set; }
        public uint ID { get; set; }
        public ushort PathID { get; set; }
        public int NodeIndex { get; set; }
        public ushort ContinentID { get; set; }
        public byte Flags { get; set; }
        public uint Delay { get; set; }
        public int ArrivalEventID { get; set; }
        public int DepartureEventID { get; set; }
    }
}
