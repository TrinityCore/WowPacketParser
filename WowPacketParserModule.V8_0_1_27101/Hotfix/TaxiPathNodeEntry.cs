using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiPathNode)]
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
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, true)]
        public ushort ArrivalEventID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public uint ArrivalEventId { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, true)]
        public ushort DepartureEventID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public int DepartureEventId { get; set; }
    }
}
