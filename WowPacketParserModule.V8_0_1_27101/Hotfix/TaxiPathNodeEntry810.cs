using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_1_0_28724.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiPathNode, ClientVersionBuild.V8_1_0_28724)]
    public class TaxiPathNodeEntry
    {
        [HotfixArray(3)]
        public float[] Loc { get; set; }
        public int ID { get; set; }
        public ushort PathID { get; set; }
        public int NodeIndex { get; set; }
        public ushort ContinentID { get; set; }
        public byte Flags { get; set; }
        public int Delay { get; set; }
        public uint ArrivalEventID { get; set; }
        public int DepartureEventID { get; set; }
    }
}
